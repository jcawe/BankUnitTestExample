using GoodBankApp.Models;

namespace GoodBankApp.Managers
{
    public interface IAccountManager
    {
        void Withdraw(Account account, decimal money);
        void Deposit(Account account, decimal money);
        Account OpenAccount(Owner owner, decimal money = default(decimal));
        void ChangeOwner(Account account, Owner owner);
    }
}