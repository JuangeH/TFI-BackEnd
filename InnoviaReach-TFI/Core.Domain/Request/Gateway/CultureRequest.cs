using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Request.Gateway
{
    public class CultureRequest
    {
        public string culture { get; set; }
        public string redirectUri { get; set; }
    }
}
