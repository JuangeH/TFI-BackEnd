using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Transversal.Extensions
{
    public static class IEnumerableExtensions
    {
        public static string ToString(this IEnumerable<string> source, string separationStr)
        {
            var array = source.ToArray();
            string str = "";

            for (int i = 0; i < array.Count(); i++)
            {
                str = $"{str}{array[i]}";

                bool isLastItem = i == (array.Count() - 1);

                if (!isLastItem)
                    str = $"{str}{separationStr}";
            }

            return str;
        }
        public static string ToString(this IEnumerable<object> source, string separationStr)
        {
            var array = source.ToArray();
            string str = "";

            for (int i = 0; i < array.Count(); i++)
            {
                str = $"{str}{array[i]}";

                bool isLastItem = i == (array.Count() - 1);

                if (!isLastItem)
                    str = $"{str}{separationStr}";
            }

            return str;
        }
    }
}
