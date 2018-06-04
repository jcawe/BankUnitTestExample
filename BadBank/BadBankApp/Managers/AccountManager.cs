using System;
using BadBankApp.Models;

namespace BadBankApp.Managers
{
    public class AccountManager
    {
        public AccountManager() { }

        private bool CheckWithdraw(Account account, decimal money)
            => account.Money >= money;

        public void Withdraw(Account account, decimal money)
        {
            if (money < 0) throw new ArgumentException($"{nameof(money)} cannot be negative");
            if (account == null) throw new ArgumentNullException(nameof(account));
            if (!CheckWithdraw(account, money)) throw new InvalidOperationException("The account doesn't have enought money");

            account.Money -= money;
        }
        public void Deposit(Account account, decimal money)
        {
            if (money < 0) throw new ArgumentException($"{nameof(money)} cannot be negative");
            if (account == null) throw new ArgumentNullException(nameof(account));

            account.Money += money;
        }
        public Account OpenAccount(Owner owner, decimal money = default(decimal))
            => new Account(Guid.NewGuid(), money, owner);

        public void ChangeOwner(Account account, Owner owner)
        {
            if (account == null) throw new ArgumentNullException(nameof(account));
            if (owner == null) throw new ArgumentNullException(nameof(owner));
            if (account.Owner == owner) throw new InvalidOperationException($"The account '{account.Id}' is already owner by {owner.FullName}");

            account.Owner = owner;
        }
    }
}