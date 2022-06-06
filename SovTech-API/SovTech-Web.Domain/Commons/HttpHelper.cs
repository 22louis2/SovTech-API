using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SovTech_Web.Domain.Commons
{
    public static class HttpHelper
    {
        public static Tuple<string, StringContent> BuildReqContent<T>(HttpClient client, string baseUrl, T model, string partOfUrl)
        {
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            var serializedModel = JsonConvert.SerializeObject(model);
            var currentUrl = Path.Combine(client.BaseAddress.ToString(), partOfUrl);

            var content = new StringContent(serializedModel, Encoding.UTF8, "application/json");
            return new Tuple<string, StringContent>(currentUrl, content);
        }

        public static async Task<Tuple<string, HttpResponseMessage>> GetContentAsync<T>(string baseUrl, T model, string partOfUrl)
        {
            var response = new HttpResponseMessage();
            using(var client = new HttpClient())
            {
                var contents = BuildReqContent(client, baseUrl, model, partOfUrl);
                response = await client.GetAsync(contents.Item1);
            }

            var responseObj = await response.Content.ReadAsStringAsync();
            return new Tuple<string, HttpResponseMessage>(responseObj, response);
        }
    }
}
