using GrainInterfaces;
using GrainInterfaces.Models;
using Orleans.Runtime;
using System.Transactions;

namespace Grains
{
    public sealed class AccountGrain : Grain, IAccountGrain
    {
        

        private readonly IPersistentState<AccountInfo> state;
        public AccountGrain
            (
            [PersistentState("account", "OrleansStorage")] IPersistentState<AccountInfo> state
            )
        {
            this.state = state;
        }
        public async ValueTask<bool> CreateAccount()
        {
            if (state.State.AccountId == 0)
            {
                state.State = new()
                {
                    Balance = 0,
                    AccountId = this.GetPrimaryKeyLong()

                };
                await state.WriteStateAsync();

                return true;
            }
            return false;
        }

        public async ValueTask<bool> Deposit(decimal amount)
        {
            state.State.Balance += amount;
            await state.WriteStateAsync();

            return true;
        }

        public ValueTask<decimal> GetBalance()
        {
            return ValueTask.FromResult(state.State.Balance);
        }

        public async ValueTask<bool> Withdraw(decimal amount)
        {
            if(state.State.Balance >= amount)
            {
                state.State.Balance -= amount;
                await state.WriteStateAsync();
                return true;
            }
            return false;
        }

        //[Transaction(TransactionOption.Create)]
        //public async Task<bool> Transfer(int fromUserId, int toUserId, decimal amount)
        //{
        //    var fromUserGrain = GrainFactory.GetGrain<IAccountGrain>(fromUserId);
        //    var toUserGrain = GrainFactory.GetGrain<IAccountGrain>(toUserId);

        //    if (await fromUserGrain.Withdraw(amount))
        //    {
        //        await toUserGrain.Deposit(amount);
        //        return true;
        //    }
        //    return false;
        //}
    }


}
