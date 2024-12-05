using Prova.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace Prova.Service
{
    public class WeatherService
    {   
        private string apikey = "2563f49acbb9f61c3d6b46e42698b17b";
        private WeatherResponse weatherResponse;
        private HttpClient httpClient;
        private ObservableCollection<WeatherResponse> weatherResponses;
        private JsonSerializerOptions jsonSerializerOptions;

        public WeatherService()
        {
            httpClient = new HttpClient();
            jsonSerializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };
        }

        public async Task<WeatherResponse> GetWeatherResponse(string city_name)
        {
            Uri uri = new Uri($"https://api.openweathermap.org/data/2.5/weather?q={city_name}&appid={apikey}");
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    WeatherResponse weatherResponse = JsonSerializer.Deserialize<WeatherResponse>(content, jsonSerializerOptions);
                    
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Erro na chamada da API: " + ex.Message);
                return null;
            }
            return weatherResponse;
        }
    }
}
