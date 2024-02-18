using GrainInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orleans;

namespace ApiClient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IClusterClient _orleansClient;

        public AccountController(IClusterClient orleansClient)
        {
            _orleansClient = orleansClient;
        }
        // POST: api/Account/{userId}/Create
        [HttpPost("{userId}/Create")]
        public async Task<ActionResult> CreateAccount(int userId)
        {
            var accountGrain = _orleansClient.GetGrain<IAccountGrain>(userId);
            bool result = await accountGrain.CreateAccount();
            if (result)
                return Ok();
            else
                return BadRequest("Unable to create account.");
        }

        // POST: api/Account/{userId}/Deposit
        [HttpPost("{userId}/Deposit")]
        public async Task<ActionResult> Deposit(int userId, [FromBody] decimal amount)
        {
            var accountGrain = _orleansClient.GetGrain<IAccountGrain>(userId);
            bool result = await accountGrain.Deposit(amount);
            if (result)
                return Ok();
            else
                return BadRequest("Unable to deposit.");
        }

        // POST: api/Account/{userId}/Withdraw
        [HttpPost("{userId}/Withdraw")]
        public async Task<ActionResult> Withdraw(int userId, [FromBody] decimal amount)
        {
            var accountGrain = _orleansClient.GetGrain<IAccountGrain>(userId);
            bool result = await accountGrain.Withdraw(amount);
            if (result)
                return Ok();
            else
                return BadRequest("Unable to withdraw.");
        }

        // GET: api/Account/{userId}/Balance
        [HttpGet("{userId}/Balance")]
        public async Task<ActionResult<decimal>> GetBalance(int userId)
        {
            var accountGrain = _orleansClient.GetGrain<IAccountGrain>(userId);
            decimal balance = await accountGrain.GetBalance();
            return Ok(balance);
        }
    }
}
