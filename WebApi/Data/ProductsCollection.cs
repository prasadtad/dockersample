using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DockerSample.Models;
using MongoDB.Driver;

namespace DockerSample.Data
{
    public class ProductsCollection : IProductsCollection
    {
        private readonly IDataCollection<Product> products;

        public ProductsCollection(IDataCollection<Product> products)
        {
            this.products = products;
        }

        public Task<IEnumerable<Product>> GetFantasticProductsAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var filterBuilder = new FilterDefinitionBuilder<Product>();
            return products.FindAsync(filterBuilder.Eq(p => p.Attribute.Fantastic.Value, true), cancellationToken);
        }

        public Task<IEnumerable<Product>> GetNonFantasticProductsAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var filterBuilder = new FilterDefinitionBuilder<Product>();
            return products.FindAsync(filterBuilder.Eq(p => p.Attribute.Fantastic.Value, false), cancellationToken);
        }

        public Task<IEnumerable<Product>> GetProductsByRatingAsync(double min, double max, CancellationToken cancellationToken = default(CancellationToken))
        {
            var filterBuilder = new FilterDefinitionBuilder<Product>();
            var filter = filterBuilder.And(
                filterBuilder.Gte(p => p.Attribute.Rating.Value, min),
                filterBuilder.Lte(p => p.Attribute.Rating.Value, max));
            return products.FindAsync(filter, cancellationToken);
        }

        public Task<IEnumerable<Product>> GetProductsByPriceAsync(double min, double max, CancellationToken cancellationToken = default(CancellationToken))
        {
            var filterBuilder = new FilterDefinitionBuilder<Product>();
            var filter = filterBuilder.And(
                filterBuilder.Gte(p => p.Price, min),
                filterBuilder.Lte(p => p.Price, max));
            return products.FindAsync(filter, cancellationToken);
        }
    }
}