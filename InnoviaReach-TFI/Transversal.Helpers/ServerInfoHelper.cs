using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Transversal.Helpers
{
    public static class ServerInfoHelper
    {
        public static string MapPath(string path)
        {
            return Path.Combine((string)AppDomain.CurrentDomain.GetData("ContentRootPath"), path);
        }
    }
}
