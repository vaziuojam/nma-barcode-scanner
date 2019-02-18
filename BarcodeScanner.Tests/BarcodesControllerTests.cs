using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace BarcodeScanner.Tests
{
    public class BarcodesControllerTests
    {
        [Fact]
        public void ReturnsProductFromCatalog()
        {
            var product = new Product
            {
                Barcode = "12345",
                Name = "3443564",
                Price = 12.56m,
            };
            var catalog = new Mock<IProductCatalog>();
            catalog.Setup(x => x.FindByBarcode("12345"))
                .Returns(product);
            var sut = new BarcodesController(catalog.Object);

            var result = sut.ScanBarcode("12345");
            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(product, ok.Value);
        }
        
        [Fact]
        public void ReturnsNotFoundWhenNoProductInCatalog()
        {
            var catalog = new Mock<IProductCatalog>();
            var sut = new BarcodesController(catalog.Object);

            var result = sut.ScanBarcode("12345");
            Assert.IsType<NotFoundResult>(result);
        }
    }
}