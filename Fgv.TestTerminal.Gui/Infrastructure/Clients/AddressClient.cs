using System;
using System.Net.Http;
using System.Threading.Tasks;
using Fgv.TestTerminal.Gui.Domain.Contracts.Request;
using Fgv.TestTerminal.Gui.Domain.Contracts.Response;
using Newtonsoft.Json;

namespace Fgv.TestTerminal.Gui.Infrastructure.Clients
{
    public class AddressClient
    {
        public HttpRequestMessage HttpRequestMessage { get; }
        public HttpClient HttpClient { get; }

        public AddressClient()
        {
            HttpClient = new HttpClient();
            HttpRequestMessage = new HttpRequestMessage
            {
                RequestUri = new Uri("https://viacep.com.br/ws/"),
                Method = HttpMethod.Get
            };
        }

        public async Task<AddressResponse> GetAddressInfo(AddressRequest contract)
        {
            HttpRequestMessage.RequestUri = new Uri($"{HttpRequestMessage.RequestUri}{contract.Cep}/json");

            var response = await HttpClient.SendAsync(HttpRequestMessage);
            if (response.IsSuccessStatusCode == false) return new AddressResponse();

            var result = await response.Content.ReadAsStringAsync();
            
            return JsonConvert.DeserializeObject<AddressResponse>(result);
        }
    }
}