using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.AzureTranslator
{
    public class AzureTranslatorService: IAzureTranslatorService
    {
        static string Key = "YOUR KEY";
        static string EndPoint = "https://api.cognitive.microsofttranslator.com/translate?api-version=3.0";
        static string ConvertTo = "&to=en";

        public async Task<List<AzureTranslatorModel>> Execute(List<AzureTranslatorRequestBody> requestBody)
        {
            try
            {
                var body = requestBody;
                var requestBodyObject = JsonConvert.SerializeObject(body);

                using(var client = new HttpClient())
                using (var request = new HttpRequestMessage())
                {
                    request.Method = HttpMethod.Post;
                    request.RequestUri = new Uri($"{EndPoint}{ConvertTo}");
                    request.Content = new StringContent(requestBodyObject, Encoding.UTF8, "application/json");
                    request.Headers.Add("Ocp-Apim-Subscription-Key", Key);

                    var response = await client.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseBody = await response.Content.ReadAsStringAsync();
                        var model = JsonConvert.DeserializeObject<List<AzureTranslatorModel>> (responseBody);
                        return model;
                    }
                    return new List<AzureTranslatorModel>();
                }
            }
            catch (Exception)
            {
                return new List<AzureTranslatorModel>();
            }
        }
    }
}
