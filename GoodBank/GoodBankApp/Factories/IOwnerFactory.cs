using System;
using GoodBankApp.Models;

namespace GoodBankApp.Factories
{
    public interface IOwnerFactory
    {
         Owner Create(Guid id, string firstName, string lastName);
    }
}