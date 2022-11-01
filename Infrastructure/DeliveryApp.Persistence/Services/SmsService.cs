using DeliveryApp.Application.Abstractions.Services;
using DeliveryApp.Infrastructure.Services;
using Infobip.Api.Client;
using Infobip.Api.Client.Api;
using System.Net.Http.Headers;
using System.Text;

namespace DeliveryApp.Persistence.Services
{
    public class SmsService : ISmsService
    {

        private static readonly string BASE_URL = "https://dmz98l.api.infobip.com";
        private static readonly string API_KEY = "b7acde411a8147b08f4a44a7863f01ec-a5b8acfc-ffd7-461a-9d4a-608244e19279";

        private static readonly string SENDER = "FoodRide";
        private static readonly string RECIPIENT = "994705484825";
        private static readonly string MESSAGE_TEXT = "This is a sample message";

        public async Task<bool> Send(string recipient, string message)
        {
            
            string phoneNumber = recipient.PhoneTrim();

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BASE_URL);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("App", API_KEY);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

           
            string smessage = $@"
            {{
                ""messages"": [
                {{
                    ""from"": ""{SENDER}"",
                    ""destinations"":
                    [
                        {{
                            ""to"": ""{phoneNumber}""
                        }}
                  ],
                  ""text"": ""{message}""
                }}
              ]
            }}";

            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, "sms/2/text/advanced");
            httpRequest.Content = new StringContent(smessage, Encoding.UTF8, "application/json");

            var response = await client.SendAsync(httpRequest);
            var responseContent = await response.Content.ReadAsStringAsync();

            return true;

        }

    }
    
}
