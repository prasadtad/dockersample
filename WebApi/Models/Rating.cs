using MongoDB.Bson.Serialization.Attributes;

namespace DockerSample.Models
{
    public class Rating
    {
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("type")]
        public string Type { get; set; }

        [BsonElement("value")]
        public double Value {get; set; }
    }
}