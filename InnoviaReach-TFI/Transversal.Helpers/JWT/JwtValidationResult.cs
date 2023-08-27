using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Transversal.Helpers.ResultClasses;

namespace Transversal.Helpers.JWT
{
    public class JwtValidationResult : GenericResult
    {
       public JwtValidationResult(params string[] errors) : base(errors) { }

        public JwtValidationResult(JwtSecurityToken jwtSecurityToken, ClaimsPrincipal principal)
        {
            JwtSecurityToken = jwtSecurityToken;
            Principal = principal;
        }

        public JwtSecurityToken JwtSecurityToken { get; set; }
        public ClaimsPrincipal Principal { get; set; }
    }
}
