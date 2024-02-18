using GrainInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grains
{
    public sealed class AuditGrain : Grain, IAuditGrain
    {
        public Task RecordTransaction(int fromUserId, int toUserId, decimal amount, bool success)
        {
            Console.WriteLine($"Audit Record: {fromUserId} -> {toUserId}, Amount: {amount}, Success: {success}");
            // ToDo add some database implementation here
            return Task.CompletedTask;
        }
    }
}
