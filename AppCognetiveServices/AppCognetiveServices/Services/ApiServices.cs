using AppCognetiveServices.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppCognetiveServices.Services
{
    public class ApiServices
    {
        private const string urlBase = "https://southcentralus.api.cognitive.microsoft.com/text/analytics/v2.0";
        private const string suffix = "sentiment";

        public async Task<Response> Post<T>(T model)
        {
            try
            {
                // C# to JSON
                var request = JsonConvert.SerializeObject(model);

                // Body
                var content = new StringContent(
                    request, Encoding.UTF8,
                    "application/json");

                var client = new HttpClient();

                // Request headers
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "xxxxxxxx");

                //client.BaseAddress = new Uri(urlBase);

                var url =urlBase +"/" + suffix;

                // Consumo de servicio
                var response = await client.PostAsync(url, content);

                // Leer el response
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                // JSON to C#
                return JsonConvert.DeserializeObject<Response>(result); ;
            }
            catch
            {
                return null;
            }
        }
    }
}