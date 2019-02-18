using Microsoft.AspNetCore.Mvc;

namespace BarcodeScanner
{
    public class Product
    {
        public string Barcode { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
    
    [Route("products")]
    public class ProductsController
    {
        private IProductCatalog _catalog;

        public ProductsController(IProductCatalog catalog)
        {
            _catalog = catalog;
        }


        [HttpPut]
        public void SaveProducts(Product[] products)
        {
            _catalog.SaveProducts(products);
        }
    }
}