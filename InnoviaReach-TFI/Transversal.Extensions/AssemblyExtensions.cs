using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Transversal.Extensions
{
    public static class AssemblyExtensions
    {
        public static IEnumerable<Type> GetTypes(this Assembly source, params Func<Type, bool>[] predicates)
            => predicates.Aggregate(source?.GetTypes(), (types, predicate) => types?.Where(predicate)?.ToArray());

        public static Type FindType(this Assembly source, params Func<Type, bool>[] predicates)
            => source?.GetTypes(predicates).FirstOrDefault();
    }
}
