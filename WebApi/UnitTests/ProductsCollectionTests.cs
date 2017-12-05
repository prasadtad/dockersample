using System;
using System.Collections.Generic;
using System.Threading;
using DockerSample.Data;
using DockerSample.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Moq;
using Xunit;

namespace DockerSample.Tests
{
    public class ProductsCollectionTests
    {
        [Fact]
        public async void TestGetProductsByPrice()
        {
            var mockProducts = new List<Product>
            {
                new Product { Id = 1, Price = 124.2 },
                new Product { Id = 2, Price = 262 },
                new Product { Id = 3, Price = 328.5 },
                new Product { Id = 4, Price = 689 }
            };
            var dataCollection = new Mock<IDataCollection<Product>>();
            dataCollection.Setup(p => p.FindAsync(ItIsMockProductFilterDefinition("{ \"price\" : { \"$gte\" : 260.0, \"$lte\" : 328.5 } }"), 
                                                  It.IsAny<CancellationToken>()))
                            .ReturnsAsync(new[] { mockProducts[1], mockProducts[2] });
            var productsCollection = new ProductsCollection(dataCollection.Object);
            Assert.Collection(await productsCollection.GetProductsByPriceAsync(260, 328.5), 
                product => Assert.Equal(2, product.Id),
                product => Assert.Equal(3, product.Id));
        }

        [Fact]
        public async void TestGetFantasticProducts()
        {
            var mockProducts = new List<Product>
            {
                new Product { Id = 1, Attribute = new Models.Attribute { Fantastic = new Fantastic { Value = false } } },
                new Product { Id = 2, Attribute = new Models.Attribute { Fantastic = new Fantastic { Value = true } } }
            };
            var dataCollection = new Mock<IDataCollection<Product>>();
            dataCollection.Setup(p => p.FindAsync(ItIsMockProductFilterDefinition("{ \"attribute.fantastic.value\" : true }"), 
                                                  It.IsAny<CancellationToken>()))
                            .ReturnsAsync(new[] { mockProducts[1] });
            var productsCollection = new ProductsCollection(dataCollection.Object);
            Assert.Collection(await productsCollection.GetFantasticProductsAsync(), 
                product => Assert.Equal(2, product.Id));
        }

        [Fact]
        public async void TestGetNonFantasticProducts()
        {
            var mockProducts = new List<Product>
            {
                new Product { Id = 1, Attribute = new Models.Attribute { Fantastic = new Fantastic { Value = false } } },
                new Product { Id = 2, Attribute = new Models.Attribute { Fantastic = new Fantastic { Value = true } } }
            };
            var dataCollection = new Mock<IDataCollection<Product>>();
            dataCollection.Setup(p => p.FindAsync(ItIsMockProductFilterDefinition("{ \"attribute.fantastic.value\" : false }"), 
                                                  It.IsAny<CancellationToken>()))
                            .ReturnsAsync(new[] { mockProducts[0] });
            var productsCollection = new ProductsCollection(dataCollection.Object);
            Assert.Collection(await productsCollection.GetNonFantasticProductsAsync(), 
                product => Assert.Equal(1, product.Id));
        }

        [Fact]
        public async void TestGetProductsByRating()
        {
            var mockProducts = new List<Product>
            {
                new Product { Id = 1, Attribute = new Models.Attribute { Rating = new Rating { Value = 124.2 } } },
                new Product { Id = 2, Attribute = new Models.Attribute { Rating = new Rating { Value = 262 } } },
                new Product { Id = 3, Attribute = new Models.Attribute { Rating = new Rating { Value = 328.5 } } },
                new Product { Id = 4, Attribute = new Models.Attribute { Rating = new Rating { Value = 689 } } }
            };
            var dataCollection = new Mock<IDataCollection<Product>>();
            dataCollection.Setup(p => p.FindAsync(ItIsMockProductFilterDefinition("{ \"attribute.rating.value\" : { \"$gte\" : 260.0, \"$lte\" : 328.5 } }"), 
                                                  It.IsAny<CancellationToken>()))
                            .ReturnsAsync(new[] { mockProducts[1], mockProducts[2] });
            var productsCollection = new ProductsCollection(dataCollection.Object);
            Assert.Collection(await productsCollection.GetProductsByRatingAsync(260, 328.5), 
                product => Assert.Equal(2, product.Id),
                product => Assert.Equal(3, product.Id));
        }

        // Note: Ideally, we wouldn't compare json strings
        private FilterDefinition<Product> ItIsMockProductFilterDefinition(string json) 
        {
            return It.Is<FilterDefinition<Product>>(fd => ToJson(fd) == json);
        }

        private static string ToJson(FilterDefinition<Product> filterDefinition)
        {
            var serializerRegistry = BsonSerializer.SerializerRegistry;
            var documentSerializer = serializerRegistry.GetSerializer<Product>();
            return filterDefinition.Render(documentSerializer, serializerRegistry).ToJson();
        }
    }
}