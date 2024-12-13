using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transversal.Extensions
{
    public static class LoggerExtensions
    {
        public static void LogBusiness(this ILogger logger, string message)
        {
            var eventId = new EventId(2000, "Business");
            var properties = new Dictionary<string, object>
            {
                { "Level", "Business" },
                { "Category", "Business" }  // Asegúrate de incluir esto en el log.
            };
            var state = new LogState(message, properties);
            logger.Log(LogLevel.Information, eventId, state, null, (s, e) => s.Message);
        }
    }

    class LogState
    {
        public string Message { get; }
        public IReadOnlyDictionary<string, object> Properties { get; }

        public LogState(string message, IDictionary<string, object> properties)
        {
            Message = message;
            Properties = new ReadOnlyDictionary<string, object>(properties);
        }

        public override string ToString()
        {
            return Message; // Esto asegura que el mensaje base se loguee correctamente.
        }
    }
}
