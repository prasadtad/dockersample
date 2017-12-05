using MongoDB.Bson.Serialization.Attributes;

namespace DockerSample.Models
{
    public class Attribute
    {
        [BsonElement("fantastic")]
        public Fantastic Fantastic { get; set; }

        [BsonElement("rating")]
        public Rating Rating { get; set; }
    }
}