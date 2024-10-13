using Core.Domain.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Models
{
    public class SteamAccountModel
    {
        public int SteamAccount_ID { get; set; }
        public string steamid { get; set; }
        public string? ApiKey { get; set; }
        public string personaname { get; set; }
        public string avatarfull { get; set; }
        public string profileurl { get; set; }
        public string User_ID { get; set; }
        public Users users { get; set; }
    }
}
