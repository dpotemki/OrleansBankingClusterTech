using GrainInterfaces;
using GrainInterfaces.Models;
using Orleans.Runtime;
using System.Security.AccessControl;
using System.Transactions;

namespace Grains
{
    public class TransactionGrain : Grain, ITransactionGrain
    {
        private readonly IPersistentState<TransactionInfo> _transactionState;
        public TransactionGrain
            (
            [PersistentState("transaction", "OrleansStorage")] IPersistentState<TransactionInfo> transactionState

            )
        {
            _transactionState = transactionState;
        }
       
        [Transaction(TransactionOption.Create)]

        public async Task<bool> Transfer(int fromUserId, int toUserId, decimal amount)
        {
            if (_transactionState.State.TransactionId != Guid.Empty 
                ||
                _transactionState.State.IsCompleted == true
                )
            {
                // guarantee of idempotency, the second transaction with a similar ID will simply not go through
                return false;
            }
            

            var fromUserGrain = GrainFactory.GetGrain<IAccountGrain>(fromUserId);
            var toUserGrain = GrainFactory.GetGrain<IAccountGrain>(toUserId);
            var notificationGrain = GrainFactory.GetGrain<INotificationGrain>("");

            var auditGrain = GrainFactory.GetGrain<IAuditGrain>(Guid.NewGuid());

            bool state = false;
            if (await fromUserGrain.Withdraw(amount))
            {
                await toUserGrain.Deposit(amount);
                await auditGrain.RecordTransaction(fromUserId, toUserId, amount, true);
                await notificationGrain.EnqueueNotification($"Money transfered from {fromUserId} to {fromUserId}.");

                state = true;
            }
            else
            {
                await notificationGrain.EnqueueNotification($"Not enough money from {fromUserId} to {fromUserId}.");
                await auditGrain.RecordTransaction(fromUserId, toUserId, amount, false);
                state = false;
            }
            
            _transactionState.State = new TransactionInfo
            {
                TransactionId = this.GetPrimaryKey(),
                FromUserId = fromUserId,
                ToUserId = toUserId,
                Amount = amount,
                IsCompleted = state
            };
            await _transactionState.WriteStateAsync();
            return state;

        }
    }


}
