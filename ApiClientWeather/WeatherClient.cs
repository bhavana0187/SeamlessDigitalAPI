using ApplicationCore.Interfaces.Services.Weather;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Text.Json;


namespace ApiClientWeather
{
    public partial class WeatherClient
    {
        private readonly HttpClient _httpClient;
        private Uri _baseEndpoint { get; set; }
        private bool _loggedIn;
        private DateTime _loginTimeout;
        private Uri _loginUrl;       
        private string _authHeader;
        private string _apiKey;
        public WeatherClient(string weatherApiBaseUrl,string weatherApiKey) 
        {
            _httpClient = new HttpClient();
            _baseEndpoint = new Uri(weatherApiBaseUrl);
            _apiKey = weatherApiKey;
        } 
        protected async Task<T> GetAsync<T>(Uri requestUrl)
        {
            string data = "";           
            try
            {
                data = await _httpClient.GetStringAsync(requestUrl);                          
            }
            catch (Exception ex)
            {
            }
            finally
            {
            }
            //Convert the JSON into an object
            return JsonSerializer.Deserialize<T>(data)!;

        }

        protected Uri CreateRequestUri(string relativePath)
        {
            var endpoint = new Uri(_baseEndpoint, relativePath);          
            return endpoint;
        }
    }
}
