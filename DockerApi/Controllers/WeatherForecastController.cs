using DockerDataModel;
using EMI.Infrastructure.Logger;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace DockerApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private NetCoreEFTestDbContext db;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, NetCoreEFTestDbContext db)
        {
            _logger = logger;
            this.db = db;
        }

        [HttpGet]
        public void Test()
        {
            this.db.UserInfo.Add(new UserInfo { UserName = "π‹¿Ì‘±", UserID = "admin" });
            this.db.SaveChanges();
            LoggerForNetCore.Write($"{this.Request.Host.Value}");
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<string> Get()
        {
            return Summaries;
            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    TemperatureC = Random.Shared.Next(-20, 55),
            //    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            //})
            //.ToArray();
        }
    }
}