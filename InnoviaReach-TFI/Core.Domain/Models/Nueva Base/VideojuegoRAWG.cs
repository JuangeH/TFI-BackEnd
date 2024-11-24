using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Models.Nueva_Base
{
    public class VideojuegoRAWG
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("description")]
        public string Descripcion { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("released")]
        public DateTime? Released { get; set; }

        [JsonProperty("background_image")]
        public string BackgroundImage { get; set; }

        [JsonProperty("rating")]
        public float Rating { get; set; }

        [JsonProperty("metacritic")]
        public int? Metacritic { get; set; }

        [JsonProperty("ratings")]
        public List<Rating> Ratings { get; set; }

        //[JsonProperty("platforms")]
        //public List<PlatformInfo> Platforms { get; set; }

        [JsonProperty("parent_platforms")]
        public List<ParentPlatform> ParentPlatforms { get; set; }

        [JsonProperty("genres")]
        public List<Genre> Genres { get; set; }

        [JsonProperty("stores")]
        public List<Store> Stores { get; set; }

        [JsonProperty("tags")]
        public List<Tag> Tags { get; set; }
    }
}
