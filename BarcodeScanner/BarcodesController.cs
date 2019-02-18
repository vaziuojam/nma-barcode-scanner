using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace BarcodeScanner
{
    [Route("barcodes")]
    public class BarcodesController : ControllerBase
    {
        private IProductCatalog _catalog;

        public BarcodesController(IProductCatalog catalog)
        {
            _catalog = catalog;
        }


        [HttpGet]
        [Route("{barcode}")]
        public IActionResult ScanBarcode(string barcode)
        {
            var product = _catalog.FindByBarcode(barcode);
            if (product != null)
            {
                return Ok(product);
            }

            return NotFound();
        }
    }
}