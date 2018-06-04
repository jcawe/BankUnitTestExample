using System;
using GoodBankApp.Models;

namespace GoodBankApp.Factories
{
    public class OwnerFactory : IOwnerFactory
    {
        public Owner Create(Guid id, string firstName, string lastName) 
            => new Owner(id, firstName, lastName);
    }
}