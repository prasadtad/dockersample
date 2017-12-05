using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace DockerSample.Models
{
    public class Product
    {
        [BsonId]
        [JsonIgnore]
        public ObjectId MongoId { get; set; }

        [BsonElement("id")]
        public int Id { get; set; }

        [BsonElement("sku")]
        public string Sku { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("price")]
        public double Price { get; set; }

        [BsonElement("attribute")]
        public Attribute Attribute { get; set; }
    }
}