using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;

namespace Studev.Server.Services {
    public class ApiService
    {
        public HttpClient Client { get; }

        public ApiService(HttpClient client)
        {
            client.BaseAddress = new Uri("https://api.github.com/");
            // GitHub requires a user-agent
            client.DefaultRequestHeaders.UserAgent
                .Add(new ProductInfoHeaderValue("Studev", "0.0.0"));

            Client = client;
        }

        public async Task<JArray> GetArray(string url)
        {
            var response = await Client.GetAsync($"{Client.BaseAddress}{url}");
            var content = await response.Content.ReadAsStringAsync();
            return JArray.Parse(content);
        }

        public async Task<JObject> GetObject(string url)
        {
            var response = await Client.GetAsync($"{Client.BaseAddress}{url}");
            var content = await response.Content.ReadAsStringAsync();
            return JObject.Parse(content);
        }

        public async Task<string> GetContent(string url)
        {
            var response = await Client.GetAsync($"{Client.BaseAddress}{url}");
            return await response.Content.ReadAsStringAsync();
        }
    }
}
