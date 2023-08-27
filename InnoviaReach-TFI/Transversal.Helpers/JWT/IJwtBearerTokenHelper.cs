using System;
using System.Collections.Generic;
using System.Text;

namespace Transversal.Helpers.JWT
{
    public interface IJwtBearerTokenHelper
    {
        string CreateJwtToken(string userId, string userName, List<string> role, string participantId = "-1");

        string CreateJwtToken(string payloadData, string base64SecretKey);

        DateTime GetValidFromDate(string token);

        DateTime GetExpirationDate(string token);

        bool IsExpired(string token);

        bool IsValidJwt(string token);

        JwtValidationResult ValidateJwtToken(string jwtToken, bool validateExpiration = false);
    }
}
