using System.Collections.Generic;
using System.IO;

namespace Transversal.Helpers
{
    public static class HtmlDocumentHelper
    {

        public static string GetHtmlDocument(string htmlName, string RootPath)
        {
            string body;
            ////string pathTemplates = Path.Combine(ServerInfoHelper.MapPath(RootPath), "Templates");
            //using (StreamReader sr = new StreamReader(Path.Combine("D:/Repositorios-GitGUI/LPPA-TP-FINAL-G3/SistemaLogin_Back/3.Infrastructure/Infrastructure.Data/TemplatesEmail/", htmlName)))
            //{
            //    body = sr.ReadToEnd();
            //}
            return null;
        }
        public static string GetHtmlDocument(string htmlName, string RootPath, List<string> replace)
        {
            string body;
            //int count = 0;
            ////string pathTemplates = Path.Combine(ServerInfoHelper.MapPath(RootPath), "Templates");
            //using (StreamReader sr = new StreamReader(Path.Combine("D:/Repositorios-GitGUI/LPPA-TP-FINAL-G3/SistemaLogin_Back/3.Infrastructure/Infrastructure.Data/TemplatesEmail/", htmlName)))
            //{
            //    body = sr.ReadToEnd();
            //}
            //replace.ForEach(x => { body = body.Replace("{"+count+"}",x); count++; });

            return null;
        }
    }
}
