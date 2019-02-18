using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BarcodeScanner.AcceptanceTests
{
    public static class HttpClientExtensions
    {
        public static Task<HttpResponseMessage> PutAsJsonAsync(this HttpClient client, string url, object o)
        {
            var json = JsonConvert.SerializeObject(o);
            var content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return client.PutAsync(url, content);
        }

        public static async Task<T> ReadAsJsonAsync<T>(this HttpContent content)
        {
            var responseJson = await content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseJson);
        }
    }
}