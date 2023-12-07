using Microsoft.AspNetCore.Mvc;
using My_Weather_App.Source.Utils;
using My_Weather_App.Source.WeatherService;

// load all .env variables
var root = Directory.GetCurrentDirectory();
DotEnv.Load(Path.Combine(root, ".env").ToString());

// load weatherService
var weatherService = new WeatherService();

// build app
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();

// add cors
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("*")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

// Add cors
app.UseCors();

// Endpoints

// returns the current weather only
app.MapGet("/current", async (
    [FromQuery(Name = "lat")] string? lat,
    [FromQuery(Name = "lon")] string? lon
    ) => await weatherService.GetCurrent(lat ?? "0", lon ?? "0", true));

// returns the current, minutely, hourly, and daily weather data
app.MapGet("/current-full", async (
    [FromQuery(Name = "lat")] string? lat,
    [FromQuery(Name = "lon")] string? lon
    ) => await weatherService.GetCurrent(lat ?? "0", lon ?? "0"));

app.MapRazorPages();

app.Run();