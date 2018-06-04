using System;
using GoodBankApp.Models;

namespace GoodBankApp.Factories
{
    public interface IAccountFactory
    {
         Account Create(Guid id, decimal money, Owner owner);
    }
}