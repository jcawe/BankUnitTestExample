using GoodBankApp.Models;

namespace GoodBankApp.Factories
{
    public class BankFactory : IBankFactory
    {
        public Bank Create(string name, params Account[] accounts) => new Bank(name, accounts);
    }
}