using _2._API.Response;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Models
{
    public class LogTableModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Timestamp")]
        public DateTime Timestamp { get; set; }

        [BsonElement("Level")]
        public string Level { get; set; }

        [BsonElement("MessageTemplate")]
        public string MessageTemplate { get; set; }

        [BsonElement("RenderedMessage")]
        public string RenderedMessage { get; set; }

        [BsonElement("Exception")]
        public string? Exception { get; set; }

        [BsonElement("Properties")]
        public LogProperties Properties { get; set; }

        [BsonElement("Renderings")]
        public Renderings Renderings { get; set; }

        [BsonElement("UtcTimestamp")]
        public string? UtcTimestamp { get; set; }
    }

    public class LogProperties
    {
        [BsonElement("MethodName")]
        public string MethodName { get; set; }

        [BsonElement("KeyId")]
        public string KeyId { get; set; }
        [BsonElement("Element")]
        public string Element { get; set; }

        [BsonElement("State")]
        public string State { get; set; }

        [BsonElement("EventId")]
        public EventId EventId { get; set; }

        [BsonElement("SourceContext")]
        public string SourceContext { get; set; }

        [BsonElement("ActionId")]
        public string ActionId { get; set; }

        [BsonElement("ActionName")]
        public string ActionName { get; set; }

        [BsonElement("RequestId")]
        public string RequestId { get; set; }

        [BsonElement("RequestPath")]
        public string RequestPath { get; set; }

        [BsonElement("userName")]
        public string UserName { get; set; }

        [BsonElement("ConnectionId")]
        public string ConnectionId { get; set; }
    }

    [BsonNoId]
    [BsonIgnoreExtraElements]
    public class EventId
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Renderings
    {
        [BsonElement("KeyId")]
        public List<Rendering> KeyId { get; set; }
        [BsonElement("State")]
        public List<KeyIdItem> State { get; set; }
    }

    public class Rendering
    {
        [BsonElement("Format")]
        public string Format { get; set; }

        [BsonElement("Rendering")]
        public string RenderingId { get; set; }
    }

    public class KeyIdItem
    {
        [BsonElement("Format")]
        public string Format { get; set; }

        [BsonElement("Rendering")]
        public string Rendering { get; set; }
    }
}