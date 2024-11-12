using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Models
{
    public class TagModel
    {
        public TagModel()
        {
            videojuegoTagModels = new HashSet<VideojuegoTagModel>();
        }
        public ICollection<VideojuegoTagModel> videojuegoTagModels { get; set; }
        public int Tag_ID { get; set; }
        public int TagRawgId { get; set; }
        public string Nombre { get; set; }
        public string Slug { get; set; }
    }
}
