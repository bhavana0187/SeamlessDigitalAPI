using ApplicationCore.Interfaces.Services.Weather;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces.Services
{
    public interface IWeatherService
    {
        Task<WeatherResponse> GetWeatherCondition(decimal latitude, decimal longitude);
    }
}
