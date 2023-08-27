using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Transversal.Extensions
{
    public static class TypeExtensions
    {
        public static bool ImplementsGenericInterface(this Type type, Type genericType)
        {
            if (type == null)
            {
                return false;
            }
            return type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == genericType);
        }
    }
}
