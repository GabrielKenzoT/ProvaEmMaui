using CommunityToolkit.Mvvm.ComponentModel;
using Prova.Models;
using Prova.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Prova.ViewModels
{
    partial class WeatherViewModel : ObservableObject 
    {
        [ObservableProperty] 
        public string description; 

        [ObservableProperty]
        public double temp; 

        [ObservableProperty]
        public double tempMin;
        
        [ObservableProperty]
        public double tempMax;
        
        [ObservableProperty]
        public int humidity; 

        [ObservableProperty]
        public string main; 

        [ObservableProperty]
        public string city_name;

        public readonly WeatherService weatherService;

        public ICommand MostrarCommand { get; }


        public WeatherViewModel() 
        { 
            Debug.WriteLine("Está funcionando");
            weatherService = new WeatherService();
            MostrarCommand = new Command(async () => await Mostrar()); 
        } 

        private async Task Mostrar() 
        {
            try
            {
                if (!string.IsNullOrEmpty(city_name)) //verifica se não está vazia ou nula
                {
                    WeatherResponse weather = await weatherService.GetWeatherResponse(city_name);
                    if (weather != null)
                    {
                        Temp = weather.Main.Temp;
                        tempMin = weather.Main.Temp_min;
                        tempMax = weather.Main.Temp_max;
                        humidity = weather.Main.Humidity;
                        description = weather.Weather.FirstOrDefault()?.Description;
                        Main = weather.Weather.FirstOrDefault()?.Main;
                    }
                    else
                    {
                        Debug.WriteLine("Resposta de serviço esta nula");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao buscar dados: {ex.Message}");
            }
        } 
    }
}
