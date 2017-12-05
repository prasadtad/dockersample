using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DockerSample.Data;
using DockerSample.Models;
using Microsoft.AspNetCore.Mvc;

namespace DockerSample.Controllers
{
    [Route("[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductsCollection productsCollection;

        public ProductsController(IProductsCollection productsCollection)
        {
            this.productsCollection = productsCollection;
        }

        [Route("price")]
        public Task<IEnumerable<Product>> GetProductsByPrice([FromQuery] double min, [FromQuery] double max, CancellationToken cancellationToken)
        {
            return productsCollection.GetProductsByPriceAsync(min, max, cancellationToken);
        }

        [Route("fantastic")]
        public Task<IEnumerable<Product>> GetFantasticProducts(CancellationToken cancellationToken)
        {
            return productsCollection.GetFantasticProductsAsync(cancellationToken);
        }

        [Route("nonfantastic")]
        public Task<IEnumerable<Product>> GetNonFantasticProducts(CancellationToken cancellationToken)
        {
            return productsCollection.GetNonFantasticProductsAsync(cancellationToken);
        }

        [Route("rating")]
        public Task<IEnumerable<Product>> GetProductsByRating([FromQuery] double min, [FromQuery] double max, CancellationToken cancellationToken)
        {
            return productsCollection.GetProductsByRatingAsync(min, max, cancellationToken);
        }
    }
}
