using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Studev.Server.Services
{
    public class ApiRequest
    {

        public async Task<JArray> GetArray(string url)
        {

            using (var http = new HttpClient())
            {
                http.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("StudevServer", "1")); // set your own values here
                var response = await http.GetAsync(url);
                var content = await response.Content.ReadAsStringAsync();
                return JArray.Parse(content); 

            }

            
        }


    }
}
