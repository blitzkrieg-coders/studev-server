using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

using Newtonsoft.Json.Linq;

namespace Studev.Server.Services {
    public class ApiService
    {
        public HttpClient Client { get; }

        public ApiService(HttpClient client, IConfiguration configuration) {
            // GitHub requires a user-agent
            client.DefaultRequestHeaders.UserAgent
                .Add(new ProductInfoHeaderValue("Studev", "1.0.0"));
            client.DefaultRequestHeaders
                .Add("Authorization", $"token {configuration.GetValue<string>("GitHubToken")}");

            Client = client;
        }

        public async Task<JArray> GetArray(string url)
        {
            var response = await Client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            return JArray.Parse(content);
        }

        public async Task<JObject> GetObject(string url)
        {
            var response = await Client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            return JObject.Parse(content);
        }

        public async Task<string> GetContent(string url)
        {
            var response = await Client.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
