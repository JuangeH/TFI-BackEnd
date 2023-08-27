using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Transversal.Extensions
{
    public static class ObjectExtensions
    {
        public static string ToJson(this object source, Formatting formatting = Formatting.None, JsonSerializerSettings settings = null)
        {
            if (source is null)
            {
                return null;
            }

            try
            {
                return settings is null ? JsonConvert.SerializeObject(source, formatting) : JsonConvert.SerializeObject(source, formatting, settings);
            }
            catch
            {
                return null;
            }
        }

        public static string ToJson(this object source, JsonSerializerSettings settings) => source.ToJson(Formatting.None, settings);
    }
}
