using DocPlanner.Models;
using DocPlanner.Models.Request;
using System.Text;
using System.Net;
using DocPlanner.Config;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace DocPlanner.Services
{
    public class ExternalService
    {
        private readonly ExternalServiceSettings _externalServiceSettings;
        private readonly HttpClient _httpClient;
        private readonly ILogger<ExternalService> _logger;

        public ExternalService(IOptions<ExternalServiceSettings> externalServiceSettings, ILogger<ExternalService> logger)
        {
            _logger = logger;
            _externalServiceSettings = externalServiceSettings.Value;

            _httpClient = new()
            {
                BaseAddress = new Uri($"{_externalServiceSettings.BaseUrl}"),
            };

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue
               ("Basic", Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(
              $"{_externalServiceSettings.User}:{_externalServiceSettings.Password}")));
            _logger = logger;
        }

        public async Task<Availability?> GetAvailabilityAsync(string monday)
        {
            try
            {
                using HttpResponseMessage response = await _httpClient.GetAsync($"GetWeeklyAvailability/{monday}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<Availability>();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving data");
            }

            return null;
        }

        public async Task<bool> PostAppointment(Appointment appointment)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(appointment), Encoding.UTF8, "application/json");
                using HttpResponseMessage response = await _httpClient.PostAsync($"TakeSlot", content);
                return response.StatusCode == HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error posting data");
            }

            return false;
        }
    }
}
