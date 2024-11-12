using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Models.Nueva_Base
{
    public class Store
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("store")]
        public StoreInfo StoreInfo { get; set; }
    }
}
