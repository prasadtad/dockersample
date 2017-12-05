using System.Threading;
using System.Threading.Tasks;
using DockerSample.Controllers;
using DockerSample.Data;
using DockerSample.Models;
using Moq;
using Xunit;

namespace DockerSample.Tests
{
    public class ProductsControllerTests
    {
        [Fact]
        public async Task TestGetProductsByPriceAsync()
        {
            var productsCollection = new Mock<IProductsCollection>();
            double mockMin = 12, mockMax = 16;
            productsCollection.Setup(c => c.GetProductsByPriceAsync(It.Is<double>(m => m == mockMin), It.Is<double>(m => m == mockMax), It.IsAny<CancellationToken>()))
                            .ReturnsAsync(new Product[] {new Product { Id = 53 }});            
            var controller = new ProductsController(productsCollection.Object);
            Assert.Collection(await controller.GetProductsByPrice(mockMin, mockMax, new CancellationToken()),
                                p => Assert.Equal(53, p.Id));
        }

        [Fact]
        public async Task TestGetFantasticProductsAsync()
        {
            var productsCollection = new Mock<IProductsCollection>();
            productsCollection.Setup(c => c.GetFantasticProductsAsync(It.IsAny<CancellationToken>()))
                            .ReturnsAsync(new Product[] {new Product { Id = 23 }});            
            var controller = new ProductsController(productsCollection.Object);
            Assert.Collection(await controller.GetFantasticProducts(new CancellationToken()),
                                p => Assert.Equal(23, p.Id));
        }

        [Fact]
        public async Task TestGetNonFantasticProductsAsync()
        {
            var productsCollection = new Mock<IProductsCollection>();
            productsCollection.Setup(c => c.GetNonFantasticProductsAsync(It.IsAny<CancellationToken>()))
                            .ReturnsAsync(new Product[] {new Product { Id = 34 }});            
            var controller = new ProductsController(productsCollection.Object);
            Assert.Collection(await controller.GetNonFantasticProducts(new CancellationToken()),
                                p => Assert.Equal(34, p.Id));
        }

        [Fact]
        public async Task TestGetProductsByRatingAsync()
        {
            var productsCollection = new Mock<IProductsCollection>();
            double mockMin = 47, mockMax = 74;
            productsCollection.Setup(c => c.GetProductsByRatingAsync(It.Is<double>(m => m == mockMin), It.Is<double>(m => m == mockMax), It.IsAny<CancellationToken>()))
                            .ReturnsAsync(new Product[] {new Product { Id = 48 }});            
            var controller = new ProductsController(productsCollection.Object);
            Assert.Collection(await controller.GetProductsByRating(mockMin, mockMax, new CancellationToken()),
                                p => Assert.Equal(48, p.Id));
        }
    }
}