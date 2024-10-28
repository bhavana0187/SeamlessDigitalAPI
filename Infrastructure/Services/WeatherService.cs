using ApplicationCore.Interfaces.Services;
using ApplicationCore.Interfaces.Services.Weather;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiClientWeather;

namespace Infrastructure.Services
{
    public class WeatherService : IWeatherService
    {     
        private readonly WeatherClient _weather;
        private readonly IConfiguration _config;

        public WeatherService(IConfiguration config)
        {
            _config= config;
            _weather = new WeatherClient(_config["Data:WeatherBaseURI"], _config["Data:WeatherApiKey"]);            
        }
        public async Task<WeatherResponse> GetWeatherCondition(decimal latitude, decimal longitude)
        {
            return await _weather.GetWeatherCondition(latitude, longitude);
        }
    }
}
