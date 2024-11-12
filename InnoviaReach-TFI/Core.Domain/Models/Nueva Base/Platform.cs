using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Models.Nueva_Base
{
    public class Platform
    {
        [JsonProperty("id")]
        public int Plataforma_Id { get; set; }

        [JsonProperty("name")]
        public string Nombre { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }
    }
}
