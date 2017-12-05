using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DockerSample.Models;

namespace DockerSample.Data
{
    public interface IProductsCollection
    {
        Task<IEnumerable<Product>> GetProductsByPriceAsync(double min, double max, CancellationToken cancellationToken = default(CancellationToken));

        Task<IEnumerable<Product>> GetFantasticProductsAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<IEnumerable<Product>> GetNonFantasticProductsAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<IEnumerable<Product>> GetProductsByRatingAsync(double min, double max, CancellationToken cancellationToken = default(CancellationToken));
    }
}