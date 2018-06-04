using System;
using GoodBankApp.Models;

namespace GoodBankApp.Managers
{
    public interface IBankManager
    {
        Bank OpenBank(string name, params Account[] accounts);
        void TransferMoney(Bank bank, decimal money, Guid sourceId, Guid targetId);
        Guid OpenAccount(Bank bank, Owner owner, decimal money = default(decimal));
        void CloseAccount(Bank bank, Guid accountId);
        void TransferAccount(Bank bank, Guid accountId, Owner owner);
    }
}