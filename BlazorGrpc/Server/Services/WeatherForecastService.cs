using BlazorGrpc.Shared;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;

namespace BlazorGrpc.Server.Services
{
    public class WeatherForecastService : WeatherForecasts.WeatherForecastsBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastService> _logger;

        public WeatherForecastService(ILogger<WeatherForecastService> logger)
        {
            _logger = logger;
        }

        public override Task<WeatherReply> GetWeather(WeatherRequest request, ServerCallContext context)
        {
            var reply = new WeatherReply();

            reply.Forecasts.Add(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = Timestamp.FromDateTime(DateTime.UtcNow.AddDays(index)),
                TemperatureC = Random.Shared.Next(Summaries.Length),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            }));

            return Task.FromResult(reply);
        }
    }
}
