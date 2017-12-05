using Autofac;
using Autofac.Core;
using DockerSample.Data;
using DockerSample.Models;
using MongoDB.Driver;

namespace DockerSample.Setup
{
    internal class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DataCollection<Product>>()
                    .As<IDataCollection<Product>>()
                    .WithParameter("collection", GetMongoCollection<Product>())
                    .SingleInstance();

            builder.RegisterType<ProductsCollection>()
                    .As<IProductsCollection>()
                    .SingleInstance();
        }

        private IMongoCollection<T> GetMongoCollection<T>() {
            var client = new MongoClient();
            return client.GetDatabase("local").GetCollection<T>("products");
        }
    }
}