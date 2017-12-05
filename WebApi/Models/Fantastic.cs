using MongoDB.Bson.Serialization.Attributes;

namespace DockerSample.Models
{
    public class Fantastic
    {
        [BsonElement("value")]
        public bool Value { get; set; }

        [BsonElement("type")]
        public int Type { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }
    }
}