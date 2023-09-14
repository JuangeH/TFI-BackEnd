using System;
using Transversal.Helpers.JWT;

namespace Api.JwT
{
    public class JwtBearerTokenSettings : IJwtBearerTokenSettings
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecretKey { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
