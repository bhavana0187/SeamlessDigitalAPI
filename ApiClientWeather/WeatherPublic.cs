using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiClientWeather
{
    public partial class WeatherClient
    { 
        public async Task<ApplicationCore.Interfaces.Services.Weather.WeatherResponse> GetWeatherCondition(decimal latitude,decimal longitude)
        { 
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"current.json?key={_apiKey}&q={latitude},{longitude}"));
            return await GetAsync<ApplicationCore.Interfaces.Services.Weather.WeatherResponse>(requestUrl);
        }        
    }
}
