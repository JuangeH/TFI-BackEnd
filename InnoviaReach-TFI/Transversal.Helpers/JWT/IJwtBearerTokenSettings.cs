using System;
using System.Collections.Generic;
using System.Text;

namespace Transversal.Helpers.JWT
{
    public interface IJwtBearerTokenSettings
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecretKey { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
