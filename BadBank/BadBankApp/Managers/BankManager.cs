using System;
using System.Linq;
using BadBankApp.Models;

namespace BadBankApp.Managers
{
    public class BankManager
    {
        readonly AccountManager _accountManager;

        public BankManager() => _accountManager = new AccountManager();

        private Account SearchAccount(Bank bank, Guid id)
        {
            if (bank == null) throw new ArgumentNullException(nameof(bank));
            return bank.Accounts.FirstOrDefault(x => x.Id == id) ?? throw new Exception($"Account with id '{id}' not found");
        }

        public void TransferMoney(Bank bank, decimal money, Guid sourceId, Guid targetId)
        {
            var source = SearchAccount(bank, sourceId);
            var target = SearchAccount(bank, targetId);

            _accountManager.Withdraw(source, money);
            _accountManager.Deposit(target, money);
        }

        public Guid OpenAccount(Bank bank, Owner owner, decimal money = default(decimal))
        {
            var account = _accountManager.OpenAccount(owner, money);
            bank.Accounts.Add(account);

            return account.Id;
        }

        public void CloseAccount(Bank bank, Guid accountId)
        {
            var account = SearchAccount(bank, accountId);
            bank.Accounts.Remove(account);
        }

        public void TransferAccount(Bank bank, Guid accountId, Owner owner)
        {
            var account = SearchAccount(bank, accountId);

            _accountManager.ChangeOwner(account, owner);
        }
    }
}