using GrainInterfaces.Models;

namespace GrainInterfaces
{
    public interface IAccountGrain :  IGrainWithIntegerKey
    {
        ValueTask<bool> Deposit(decimal amount);
        ValueTask<bool> Withdraw(decimal amount);
        ValueTask<bool> CreateAccount();
        ValueTask<decimal> GetBalance();
    }
}
