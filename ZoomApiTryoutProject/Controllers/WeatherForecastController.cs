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
    public class WeatherForecastController : Controller
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
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [Route("api/CreateInstantZoomMeeting")]
        [HttpGet]
        public async Task<InstantMeeting> CreateInstantZoomMeeting()
        {
            InstantMeeting meeting = await ZoomMeeting.CreateInstantZoomMeeting();

            return meeting;
        }

        [Route("api/Create5MinScheduledMeeting")]
        [HttpGet]
        public async Task<ScheduledMeeting> Create5MinScheduledMeeting()
        {
            ScheduledMeeting meeting = await ZoomMeeting.Create5MinScheduledMeeting();

            return meeting;
        }
    }
}
