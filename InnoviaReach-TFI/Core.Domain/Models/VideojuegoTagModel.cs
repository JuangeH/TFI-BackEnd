using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Models
{
    public class VideojuegoTagModel
    {
        public int ID { get; set; }
        public int Tag_ID { get; set; }
        public int Videojuego_ID { get; set; }
        public VideojuegoModel? videojuego { get; set; }
        public TagModel? tagModel { get; set; }
    }
}
