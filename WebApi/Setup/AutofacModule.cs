using Autofac;
using Autofac.Core;
using DockerSample.Data;
using DockerSample.Models;

namespace DockerSample.Setup
{
    internal class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DataCollection<Product>>()
                    .As<IDataCollection<Product>>()
                    .SingleInstance();

            builder.RegisterType<ProductsCollection>()
                    .As<IProductsCollection>()
                    .SingleInstance();
        }
    }
}