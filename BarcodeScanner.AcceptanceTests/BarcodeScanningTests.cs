using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace BarcodeScanner.AcceptanceTests
{
    public class BarcodeScanningTests : IDisposable
    {
        private TestServer _server;
        private HttpClient _client;

        public BarcodeScanningTests()
        {
            File.Delete("products.db");
            _server = new TestServer(Program.CreateWebHostBuilder(new string[0]));
            _client = _server.CreateClient();
        }
        

        [Theory]
        [InlineData("12345", "Water bottle", "1.99")]
        [InlineData("45456", "Banana", "2.49")]
        public async void CanScanPreviouslySavedProduct(string barcode, string name, string price)
        {
            //act
            // PUT /products
            // [...]
            var product = new
            {
                barcode,
                name,
                price = decimal.Parse(price),
            };
            
            var products = new[]
            {
                product
            };
            var response = await _client.PutAsJsonAsync("/products", products);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            response = await _client.GetAsync("/barcodes/" + barcode);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            await AssertContentEquals(response.Content, product);
        }
        
        [Fact]
        public async void UnknownBarcodeReturnsHttp404()
        {
            //act
            var response = await _client.GetAsync("/barcodes/46567");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        public async Task AssertContentEquals<T>(HttpContent content, T expected)
        {
            var actual = await content.ReadAsJsonAsync<T>();
            Assert.Equal(expected, actual);
        }

        public void Dispose()
        {
            _client.Dispose();
            _server.Dispose();
        }
    }
}