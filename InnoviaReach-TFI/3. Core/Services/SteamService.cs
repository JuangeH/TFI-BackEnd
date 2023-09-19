using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace _3._Core.Services
{
    public class SteamService
    {
        HttpClient httpClient = new HttpClient();

        public async Task CargarBaseVideojuegos()
        {
            var resultado = await httpClient.GetAsync("https://api.steampowered.com/ISteamApps/GetAppList/v2/");

        }
        
    }
}
