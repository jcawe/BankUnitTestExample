using System;
using System.Linq;
using BadBankApp.Models;

namespace BadBankApp.Managers
{
    public class BankManager
    {
        readonly AccountManager _accountManager;

        public BankManager() => _accountManager = new AccountManager();

        private Account SearchAccount(Guid id)
        {
            return Bank.Instance.Accounts.FirstOrDefault(x => x.Id == id) ?? throw new Exception($"Account with id '{id}' not found");
        }

        public void TransferMoney(decimal money, Guid sourceId, Guid targetId)
        {
            var source = SearchAccount(sourceId);
            var target = SearchAccount(targetId);

            _accountManager.Withdraw(source, money);
            _accountManager.Deposit(target, money);
        }

        public Guid OpenAccount(Owner owner, decimal money = default(decimal))
        {
            var account = _accountManager.OpenAccount(owner, money);
            Bank.Instance.Accounts.Add(account);

            return account.Id;
        }

        public void CloseAccount(Guid accountId)
        {
            var account = SearchAccount(accountId);
            Bank.Instance.Accounts.Remove(account);
        }

        public void TransferAccount(Guid accountId, Owner owner)
        {
            var account = SearchAccount(accountId);

            _accountManager.ChangeOwner(account, owner);
        }
    }
}