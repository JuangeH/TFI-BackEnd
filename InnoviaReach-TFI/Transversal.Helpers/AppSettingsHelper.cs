using Microsoft.Extensions.Configuration;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Transversal.Helpers
{
    public static class AppSettingsHelper
    {
        //Recibimos el array, también se puede hacer para listas, etc.
        public static IConfigurationSection GetSection(this string[] Elements, IConfiguration configuration)
        {
            IConfigurationSection section = null;
            //recorremos los elementos...
            Elements.ToList().ForEach(element =>
            {
                //Si es null obtenemos la primera sección
                section = section == null ? configuration.GetSection(element) : section.GetSection(element);
            });
            return section;
        }

        public static string GetFullNameSections(this Type Tipo)
        {
            string FullName = string.Empty;
            Tipo.GetTypes().ToList().ForEach(x =>
            {
                FullName += x.FullName + ".";
            });
            return FullName;
        }

        private static IEnumerable<Type> GetTypes(this Type Tipo)
        {
            while (Tipo != null)
            {
                yield return Tipo;
                Tipo = Tipo.UnderlyingSystemType;
            }
        }

    }
}
