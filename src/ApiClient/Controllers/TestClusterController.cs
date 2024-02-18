using GrainInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiClient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestClusterController : ControllerBase
    {
        private IClusterClient _orleansClient;

        public TestClusterController(IClusterClient orleansClient)
        {
            _orleansClient = orleansClient;
        }

        [HttpGet("SayClusterHi")]
        public async Task SayClusterHi()
        {
            for (var i = 0; i < 100; i++)
            {
                IHello friend = _orleansClient.GetGrain<IHello>(i);
                string response = await friend.SayHello($"Hi friend {i}!");
                Console.WriteLine(response);
            }
        }
    }
}
