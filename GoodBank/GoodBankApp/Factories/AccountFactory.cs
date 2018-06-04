using System;
using GoodBankApp.Models;

namespace GoodBankApp.Factories
{
    public class AccountFactory : IAccountFactory
    {
        public Account Create(Guid id, decimal money, Owner owner) => new Account(id, money, owner);
    }
}