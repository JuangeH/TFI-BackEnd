using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Models.Nueva_Base
{
    public class PlatformInfo
    {
        [JsonProperty("platform")]
        public Platform Platform { get; set; }

        [JsonProperty("released_at")]
        public string ReleasedAt { get; set; }

        [JsonProperty("requirements_en")]
        public RequirementsEn RequirementsEn { get; set; }
    }
}
