using Microsoft.Extensions.Options;
using MotChecking.Models;
using System.Text.Json;

namespace MotChecking.Services
{
    public class MotApiService(IOptions<ApiConfig> apiConfig)
    {
        const string urlBase = "https://beta.check-mot.service.gov.uk/trade/vehicles/mot-tests?registration=";

        private IOptions<ApiConfig> ApiConfig { get; } = apiConfig;

        public async Task<VehicleInfo> GetVehicleInfoAsync(string registrationNumber)
        {
            if (string.IsNullOrWhiteSpace(registrationNumber))
            {
                throw new ArgumentException("Registration number cannot be empty");
            }

            string apiUrl = urlBase + registrationNumber;

            HttpResponseMessage response = await GetMessage(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                response.Dispose();

                return JsonSerializer.Deserialize<List<VehicleInfo>>(responseBody).FirstOrDefault();
            }

            else
            {
                throw new HttpRequestException($"Failed to retrieve vehicle information. Status code: {response.StatusCode}");
            }
        }

        public async Task<HttpResponseMessage> GetMessage(string apiUrl)
        {
            HttpClientHandler clientHandler = new()
            {
                UseCookies = false,
            };
            HttpClient client = new(clientHandler);
            HttpRequestMessage request = new()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(apiUrl),
                Headers =
                {
                    { "x-api-key", ApiConfig.Value.ApiKey },
                },
            };
            HttpResponseMessage response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return response;
        }
    }
}
