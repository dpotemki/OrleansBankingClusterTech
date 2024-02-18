using ApiClient.Models;
using GrainInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orleans;

namespace ApiClient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private IClusterClient _orleansClient;

        public TransactionController(IClusterClient clusterClient)
        {
            _orleansClient = clusterClient;
        }

        // POST: api/Transaction/Transfer
        [HttpPost("Transfer")]
        public async Task<ActionResult> Transfer([FromBody] TransferRequest request)
        {
            var transactionGrain = _orleansClient.GetGrain<ITransactionGrain>(request.RequestId);
            bool result = await transactionGrain.Transfer(request.FromUserId, request.ToUserId, request.Amount);
            if (result)
                return Ok();
            else
                return BadRequest("Transfer failed.");
        }
    }
}
