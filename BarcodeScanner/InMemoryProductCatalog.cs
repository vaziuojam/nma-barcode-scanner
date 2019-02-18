using System.Collections.Generic;
using System.Linq;

namespace BarcodeScanner
{
    public interface IProductCatalog
    {
        void SaveProducts(IEnumerable<Product> products);
        Product FindByBarcode(string barcode);
    }
    
    public class InMemoryProductCatalog : IProductCatalog
    {
        private static Product[] Catalog = new Product[0];

        public void SaveProducts(IEnumerable<Product> products)
        {
            Catalog = products.ToArray();
        }

        public Product FindByBarcode(string barcode)
        {
            return Catalog
                .FirstOrDefault(x => x.Barcode == barcode);
        }
    }
}