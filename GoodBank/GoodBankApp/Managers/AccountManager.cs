using System;
using GoodBankApp.Factories;
using GoodBankApp.Models;

namespace GoodBankApp.Managers
{
    public class AccountManager : IAccountManager
    {
        readonly IAccountFactory _accountFatory;
        public const string SameOwnerError = "The account '{0}' is already owner by {1}";
        public const string InsufficientFundsError = "The account doesn't have enought money";
        public const string MoneyNegativeError = "money cannot be negative";
        public AccountManager(IAccountFactory accountFatory) 
            => _accountFatory = accountFatory ?? throw new ArgumentNullException(nameof(accountFatory));

        private bool CheckWithdraw(Account account, decimal money) 
            => account.Money >= money;

        public void Withdraw(Account account, decimal money)
        {
            if(money < 0) throw new ArgumentException(MoneyNegativeError);
            if(account == null) throw new ArgumentNullException(nameof(account));
            if(!CheckWithdraw(account, money)) throw new InvalidOperationException(InsufficientFundsError);

            account.Money -= money;
        }
        public void Deposit(Account account, decimal money)
        {
            if(money < 0) throw new ArgumentException(MoneyNegativeError);
            if(account == null) throw new ArgumentNullException(nameof(account));

            account.Money += money;
        }
        public Account OpenAccount(Owner owner, decimal money = default(decimal))
            => _accountFatory.Create(Guid.NewGuid(), money, owner);
        
        public void ChangeOwner(Account account, Owner owner)
        {
            if(account == null) throw new ArgumentNullException(nameof(account));
            if(owner == null) throw new ArgumentNullException(nameof(owner));
            if(account.Owner == owner) throw new InvalidOperationException(string.Format(SameOwnerError, account.Id, owner.FullName));

            account.Owner = owner;
        }
    }
}