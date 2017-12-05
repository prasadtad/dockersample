using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using DockerSample.Data;
using DockerSample.Models;
using Mongo2Go;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace DockerSample.Tests
{
    public class DataCollectionTests : IDisposable
    {
        private readonly MongoDbRunner Runner;

        private readonly IMongoDatabase MongoDatabase;

        private readonly List<Product> MockProducts;

        public DataCollectionTests() 
        {
            Runner = MongoDbRunner.Start();
            var client = new MongoClient(Runner.ConnectionString);
            MongoDatabase = client.GetDatabase("docker-sample-test");
            var products = MongoDatabase.GetCollection<Product>("products");
            MockProducts = JsonConvert.DeserializeObject<List<Product>>(File.ReadAllText(Path.Combine("IntegrationTests","MockData.json")));
            products.BulkWrite(MockProducts.Select(p => new InsertOneModel<Product>(p)));
        }

        public void Dispose()
        {
            Runner.Dispose();
        }

        [Fact]
        public async void TestFind()
        {
            var dataCollection = new DataCollection<Product>(MongoDatabase.GetCollection<Product>("products"));
            var products = (await dataCollection.FindAsync(FilterDefinition<Product>.Empty)).ToList();
            Assert.Equal(1000, products.Count);
            var inspectors = MockProducts.Select(mp => {
                return new Action<Product>(p => {
                    Assert.Equal(mp.Id, p.Id);
                    Assert.Equal(mp.Name, p.Name);
                    Assert.Equal(mp.Price, p.Price);
                    Assert.Equal(mp.Sku, p.Sku);
                    Assert.Equal(mp.Attribute.Fantastic.Name, p.Attribute.Fantastic.Name);
                    Assert.Equal(mp.Attribute.Fantastic.Type, p.Attribute.Fantastic.Type);
                    Assert.Equal(mp.Attribute.Fantastic.Value, p.Attribute.Fantastic.Value);
                    Assert.Equal(mp.Attribute.Rating.Name, p.Attribute.Rating.Name);
                    Assert.Equal(mp.Attribute.Rating.Type, p.Attribute.Rating.Type);
                    Assert.Equal(mp.Attribute.Rating.Value, p.Attribute.Rating.Value);
                });
            }).ToArray();
            Assert.Collection(products, inspectors);
        }
    }
}