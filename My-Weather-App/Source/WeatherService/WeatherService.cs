namespace My_Weather_App.Source.WeatherService
{
    using System;
    using System.Text.Json;
    using My_Weather_App.Source.Models;

    public class WeatherService
    {
        private readonly string? DefaultLattitude;
        private readonly string? DefaultLongitude;
        private readonly string? API_KEY;
        private readonly string? Lang;
        private readonly string? Units;

        public WeatherService()
        {
            Lang = "en";
            Units = "metric";
            API_KEY = Environment.GetEnvironmentVariable("WEATHER_KEY");
            DefaultLattitude = Environment.GetEnvironmentVariable("LAT") ?? "0";
            DefaultLongitude = Environment.GetEnvironmentVariable("LON") ?? "0";
        }
             
        public async Task<IResult> GetCurrent(string lat, string lon, bool notFull = false)
        {
            try
            {
                HttpClient httpClient = new ();                
               
                // fetch data from the openweather api
                using HttpResponseMessage weatherData = await httpClient
                    .GetAsync(lat == "0" || lon == "0" ? BuildUrl(notFull) :
                    BuildUrl(lat, lon, notFull));

                // store the data as a variable so it can be used
                var weatherResponseData = await weatherData.Content.ReadAsStringAsync();

                weatherResponseData = weatherResponseData.Replace("event", "_event");

                // create our OneCallApiData
                OneCallApiData data = JsonSerializer.Deserialize<OneCallApiData>(weatherResponseData)!;
                                                       
                return Results.Ok(data);
            }
            catch
            {
                return Results.BadRequest();
            }
        }
      
        private string BuildUrl(bool exclude)
        {
            var EXCLUDE = exclude ? "&exclude=minutely,hourly,daily" : "";
            var url = $"https://api.openweathermap.org/data/2.5/onecall?lat=" +
                $"{DefaultLattitude}&lon={DefaultLongitude}{EXCLUDE}&appid=" +
                $"{API_KEY}&lang={Lang}&units={Units}";

            return url;
        }
        
        private string BuildUrl(string? lat, string? lon, bool exclude)
        {
            var EXCLUDE = exclude ? "&exclude=minutely,hourly,daily" : "";

            var LAT = lat ?? DefaultLattitude ?? "0";
            var LON = lon ?? DefaultLongitude ?? "0";

            return $"https://api.openweathermap.org/data/2.5/onecall?lat={LAT}" +
                $"&lon={LON}{EXCLUDE}&appid={API_KEY}&lang={Lang}&units={Units}";
        }
    }
}