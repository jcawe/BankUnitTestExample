using System;
using System.Linq;
using GoodBankApp.Models;

namespace GoodBankApp.Managers
{
    public class BankManager
    {
        readonly IAccountManager _accountManager;

        public BankManager(IAccountManager accountManager)
            => _accountManager = accountManager ?? throw new ArgumentNullException(nameof(accountManager));

        private Account SearchAccount(Bank bank, Guid id)
        {
            if(bank == null) throw new ArgumentNullException(nameof(bank));
            return bank.Accounts.FirstOrDefault(x => x.Id == id) ?? throw new Exception($"Account with id '{id}' not found");
        }

        public void TransferMoney(Bank bank, decimal money, Guid idSource, Guid idTarget)
        {
            var source = SearchAccount(bank, idSource);
            var target = SearchAccount(bank, idTarget);

            _accountManager.Withdraw(source, money);
            _accountManager.Deposit(target, money);
        }

        public void OpenAccount(Bank bank, Owner owner, decimal money = default(decimal))
        {

        }

        public void TransferAccount(Guid idAccount, Owner owner)
            => throw new NotImplementedException();
    }
}