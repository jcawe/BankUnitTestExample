using System;
using System.Linq;
using GoodBankApp.Factories;
using GoodBankApp.Models;

namespace GoodBankApp.Managers
{
    public class BankManager : IBankManager
    {
        readonly IAccountManager _accountManager;
        readonly IBankFactory _bankFactory;
        public const string AccountNotFoundError = "Account with id '{0}' not found";

        public BankManager(IBankFactory bankFactory, IAccountManager accountManager)
        {
            _bankFactory = bankFactory ?? throw new ArgumentNullException(nameof(bankFactory));
            _accountManager = accountManager ?? throw new ArgumentNullException(nameof(accountManager));
        }

        private Account SearchAccount(Bank bank, Guid id)
        {
            if(bank == null) throw new ArgumentNullException(nameof(bank));
            return bank.Accounts.FirstOrDefault(x => x.Id == id) ?? throw new Exception(string.Format(AccountNotFoundError, id));
        }

        public Bank OpenBank(string name, params Account[] accounts)
            => _bankFactory.Create(name, accounts);
        
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