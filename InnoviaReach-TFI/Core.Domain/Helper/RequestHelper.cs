using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Core.Domain.Helper
{
    public static class RequestHelper
    {
        public static Dictionary<string, string> ToDictionary(IFormCollection col)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            foreach (string k in col.Keys)
            {
                dict.Add(k, col[k]);
            }
            return dict;
        }

        private static Dictionary<string, List<byte[]>> fileSignature = new Dictionary<string, List<byte[]>>
        {
                    { ".DOC", new List<byte[]> { new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 } } },
                    { ".DOCX", new List<byte[]> { new byte[] { 0x50, 0x4B, 0x03, 0x04 } } },
                    { ".PDF", new List<byte[]> { new byte[] { 0x25, 0x50, 0x44, 0x46 } } },
                    { ".ZIP", new List<byte[]> {
                                              new byte[] { 0x50, 0x4B, 0x03, 0x04 },
                                              new byte[] { 0x50, 0x4B, 0x4C, 0x49, 0x54, 0x55 },
                                              new byte[] { 0x50, 0x4B, 0x53, 0x70, 0x58 },
                                              new byte[] { 0x50, 0x4B, 0x05, 0x06 },
                                              new byte[] { 0x50, 0x4B, 0x07, 0x08 },
                                              new byte[] { 0x57, 0x69, 0x6E, 0x5A, 0x69, 0x70 }
                                                }
                                            },
                    { ".PNG", new List<byte[]> { new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A } } },
                    { ".JPG", new List<byte[]>
                                    {
                                              new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                                              new byte[] { 0xFF, 0xD8, 0xFF, 0xE1 },
                                              new byte[] { 0xFF, 0xD8, 0xFF, 0xE8 }
                                    }
                                    },
                    { ".JPEG", new List<byte[]>
                                        {
                                            new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                                            new byte[] { 0xFF, 0xD8, 0xFF, 0xE2 },
                                            new byte[] { 0xFF, 0xD8, 0xFF, 0xE3 }
                                        }
                                        },
                    { ".XLS", new List<byte[]>
                                            {
                                              new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 },
                                              new byte[] { 0x09, 0x08, 0x10, 0x00, 0x00, 0x06, 0x05, 0x00 },
                                              new byte[] { 0xFD, 0xFF, 0xFF, 0xFF }
                                            }
                                            },
                    { ".XLSX", new List<byte[]> { new byte[] { 0x50, 0x4B, 0x03, 0x04 } } },
                    { ".GIF", new List<byte[]> { new byte[] { 0x47, 0x49, 0x46, 0x38 } } }
        };

        public static bool IsValidFileExtension(IFormFile fileobject, string[] migrationValidExtensions)
        {
            //string[] migrationValidExtensions = { ".CSV", ".XLS", ".XLSX" };

            if (string.IsNullOrEmpty(fileobject.FileName) || fileobject == null || fileobject.Length == 0)
            {
                return false;
            }

            bool flag = false;
            string ext = Path.GetExtension(fileobject.FileName);
            if (string.IsNullOrEmpty(ext))
            {
                return false;
            }

            ext = ext.ToUpperInvariant();

            if (migrationValidExtensions.Contains(ext))
            {
                return true;
            }

            return flag;
        }

        public static async Task<T> PostRequest<T, TK>(string url, TK model)
        {
            try
            {
                T result = default(T);
                HttpClient client = new HttpClient();
                client.Timeout = TimeSpan.FromMinutes(10);
                string postBody = JsonConvert.SerializeObject(model);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.PostAsync(url, new StringContent(postBody, Encoding.UTF8, "application/json"));
                string contents = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<T>(contents);

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static async Task<T> PostRequestFiles<T>(string url, IFormCollection form)
        {
            try
            {
                MultipartFormDataContent multiContent = new MultipartFormDataContent();
                List<IFormFile> list = form.Files.ToList();
                foreach (IFormFile file in list)
                {
                    StreamContent streamContent = new StreamContent(file.OpenReadStream());
                    multiContent.Add(streamContent, file.Name, file.FileName);
                }

                T result = default(T);
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.PostAsync(url, multiContent);
                string contents = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<T>(contents);

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static async Task<T> PutRequest<T, TK>(string url, TK model)
        {
            try
            {
                T result = default(T);
                HttpClient client = new HttpClient();
                string postBody = JsonConvert.SerializeObject(model);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.PutAsync(url, new StringContent(postBody, Encoding.UTF8, "application/json"));
                string contents = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<T>(contents);

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static async Task<T> DeleteRequest<T>(string url)
        {
            try
            {
                T result = default(T);
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.DeleteAsync(url);
                string contents = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<T>(contents);

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static async Task<T> GetRequest<T>(string urlRequest)
        {
            try
            {
                T result = default;

                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(urlRequest);
                string contents = response.Content.ReadAsStringAsync().Result;
                result = JsonConvert.DeserializeObject<T>(contents);

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[16 * 1024];
            int read;
            while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, read);
            }
        }

        public static string GeneratePassword(PasswordOptions options)
        {
            int length = options.RequiredLength;

            bool nonAlphanumeric = options.RequireNonAlphanumeric;
            bool digit = options.RequireDigit;
            bool lowercase = options.RequireLowercase;
            bool uppercase = options.RequireUppercase;

            StringBuilder password = new StringBuilder();
            Random random = new Random();

            while (password.Length < length)
            {
                char c = (char)random.Next(32, 126);

                password.Append(c);

                if (char.IsDigit(c))
                    digit = false;
                else if (char.IsLower(c))
                    lowercase = false;
                else if (char.IsUpper(c))
                    uppercase = false;
                else if (!char.IsLetterOrDigit(c))
                    nonAlphanumeric = false;
            }

            if (nonAlphanumeric)
                password.Append((char)random.Next(33, 48));
            if (digit)
                password.Append((char)random.Next(48, 58));
            if (lowercase)
                password.Append((char)random.Next(97, 123));
            if (uppercase)
                password.Append((char)random.Next(65, 91));

            return password.ToString();
        }

    }
}
