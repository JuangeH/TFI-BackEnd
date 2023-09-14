using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Linq;
using Transversal.Helpers.JWT;
using System.Collections.Generic;

namespace Api.JwT
{
    public class JwtBearerTokenHelper : IJwtBearerTokenHelper
    {
        private readonly JwtBearerTokenSettings _jwtBearerTokenSettings;
        private readonly TokenValidationParameters _tokenValidationParameters;

        public JwtBearerTokenHelper(IJwtBearerTokenSettings jwtBearerTokenSettings, TokenValidationParameters tokenValidationParameters)
        {
            _jwtBearerTokenSettings = (JwtBearerTokenSettings)jwtBearerTokenSettings;
            _tokenValidationParameters = tokenValidationParameters;
        }

        #region Properties Getters

        private DateTime Expiration => IssuedAt.Add(ValidFor);

        private DateTime NotBefore => DateTime.UtcNow;

        private DateTime IssuedAt => DateTime.UtcNow;

        private TimeSpan ValidFor => _jwtBearerTokenSettings.Duration;

        private string Jti => Guid.NewGuid().ToString();

        private byte[] EncodedSecretKey => Encoding.ASCII.GetBytes(_jwtBearerTokenSettings.SecretKey);

        private string SignatureAlgorithm => SecurityAlgorithms.HmacSha256Signature;

        private SigningCredentials SigningCredentials => new SigningCredentials(new SymmetricSecurityKey(EncodedSecretKey), SignatureAlgorithm);

        #endregion

        public string CreateJwtToken(string userId, string userName, List<string> role, string participantId = "-1")
        {
            try
            {
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, userId),
                    new Claim("ParticipantId", participantId),
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(JwtRegisteredClaimNames.Iat, IssuedAt.ToString(), nameof(DateTime)),
                };
                claims.AddRange(role.Select(x => new Claim(ClaimTypes.Role, x)));

                //var claims = new Claim[]
                //{
                //new Claim(ClaimTypes.NameIdentifier, userId),
                //new Claim("ParticipantId", participantId),
                //new Claim(ClaimTypes.Name, userName),
                //new Claim(JwtRegisteredClaimNames.Iat, IssuedAt.ToString(), nameof(DateTime)),
                //new role.ForEach(x=> new Claim(ClaimTypes.Role ,x.ToString()))
                //};
                //claims.
                

                

                var securityToken = new JwtSecurityToken(
                    issuer: _jwtBearerTokenSettings.Issuer,
                    audience: _jwtBearerTokenSettings.Audience,
                    claims: claims,
                    notBefore: NotBefore,
                    expires: Expiration,
                    signingCredentials: SigningCredentials
                    );

                return new JwtSecurityTokenHandler().WriteToken(securityToken);
            }
            catch (SecurityTokenEncryptionFailedException)
            {
                return null;
            }
        }

        public string CreateJwtToken(string payloadData, string base64SecretKey)
        {
            try
            {
                var key = new SymmetricSecurityKey(Convert.FromBase64String(base64SecretKey));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var header = new JwtHeader(credentials);

                var token = new JwtSecurityToken(header, JwtPayload.Deserialize(payloadData));
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (SecurityTokenEncryptionFailedException)
            {
                return null;
            }
        }

        public DateTime GetValidFromDate(string token) => new JwtSecurityTokenHandler().ReadJwtToken(token).ValidFrom;

        public DateTime GetExpirationDate(string token) => new JwtSecurityTokenHandler().ReadJwtToken(token).ValidTo;

        public bool IsExpired(string token) => new JwtSecurityTokenHandler().ReadJwtToken(token).ValidTo.CompareTo(DateTime.UtcNow) < 0;

        public bool IsValidJwt(string token) => new JwtSecurityTokenHandler().CanReadToken(token);

        public JwtValidationResult ValidateJwtToken(string jwtToken, bool validateExpiration = false)
        {
            try
            {
                var validationParameters = _tokenValidationParameters.Clone();
                validationParameters.ValidateLifetime = validateExpiration;
                var principal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out var securityToken);
                if (!(securityToken is JwtSecurityToken jwtSecurityToken) || !jwtSecurityToken.Header.Alg.Equals(SignatureAlgorithm, StringComparison.OrdinalIgnoreCase))
                {
                    return new JwtValidationResult("Invalid token type or signature algorithm.");
                }
                return new JwtValidationResult(jwtSecurityToken, principal);
            }
            catch (SecurityTokenException ex)
            {
                return new JwtValidationResult(ex.Message);
            }
        }
    }
}
