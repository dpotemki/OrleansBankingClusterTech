using GrainInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiClient.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private IClusterClient _orleansClient;

        public WeatherForecastController(IClusterClient orleansClient)
        {
            _orleansClient = orleansClient;
        }
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        //private readonly ILogger<WeatherForecastController> _logger;

        //public WeatherForecastController(ILogger<WeatherForecastController> logger)
        //{
        //    _logger = logger;
        //}

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            var firstUserId = 10;
            var secondUserId = 11220;
            var firstCccGrain = _orleansClient.GetGrain<IAccountGrain>(firstUserId);
            var secondCccGrain = _orleansClient.GetGrain<IAccountGrain>(secondUserId);
            var balance = await firstCccGrain.GetBalance();

            await firstCccGrain.CreateAccount();
            await firstCccGrain.Deposit(100);
            await firstCccGrain.Withdraw(50);
            balance = await firstCccGrain.GetBalance();

            await secondCccGrain.CreateAccount();

            var transactionID = Guid.Parse("d3f3e3e3-3e3e-3e3e-3e3e-3e3e3e3e3e4e");
            var transactionGrain = _orleansClient.GetGrain<ITransactionGrain>(transactionID);
           
            var transactionResult = await transactionGrain.Transfer(firstUserId, secondUserId, 20);
            var firstBalance = await firstCccGrain.GetBalance();
            var secondBalance = await secondCccGrain.GetBalance();
            transactionResult = await transactionGrain.Transfer(firstUserId, secondUserId, 20);
            firstBalance = await firstCccGrain.GetBalance();
            secondBalance = await secondCccGrain.GetBalance();

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
