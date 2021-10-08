using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZoomNet.Models;

namespace ZoomApiTryoutProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        public ZoomMeeting ZoomMeeting { get; private set; }

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ZoomMeeting zoomMeeting)
        {
            _logger = logger;
            ZoomMeeting = zoomMeeting;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            InstantMeeting meeting = await ZoomMeeting.CreateZoomMeeting();


            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
