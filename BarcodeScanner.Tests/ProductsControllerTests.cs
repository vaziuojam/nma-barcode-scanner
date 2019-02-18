using System.Collections.Generic;
using Moq;
using Xunit;

namespace BarcodeScanner.Tests
{
    public class ProductsControllerTests
    {
        [Fact]
        public void ItSavesPassedInProductsInCatalog()
        {
            //MOCK
            var catalog = new Mock<IProductCatalog>();
            var sut = new ProductsController(catalog.Object);
            var products = new[]
            {
                new Product
                {
                    Barcode = "124",
                    Name = "dsfdsfg",
                    Price = 12.67m,
                },
                new Product
                {
                    Barcode = "45464",
                    Name = "sdfds",
                    Price = 34.70m,
                },
            };
            sut.SaveProducts(products);
            
            catalog.Verify(x => x.SaveProducts(products));
        }
    }
}