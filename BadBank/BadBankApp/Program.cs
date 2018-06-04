using System;
using System.Collections.Generic;
using System.Linq;
using BadBankApp.Managers;
using BadBankApp.Models;
using Newtonsoft.Json;

namespace BadBankApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var bankManager = new BankManager();

            var bank = Bank.Instance;

            Console.WriteLine($"Welcome to {bank.Name}!");
            Console.WriteLine();

            var owner1 = new Owner(Guid.NewGuid(), "Jhon", "Doe");
            var owner2 = new Owner(Guid.NewGuid(), "Jane", "Doe");

            PresentOwner(owner1);
            PresentOwner(owner2);
            Console.WriteLine();
            
            var account1 = bankManager.OpenAccount(bank, owner1, 500);
            var account2 = bankManager.OpenAccount(bank, owner1, 2000);
            var account3 = bankManager.OpenAccount(bank, owner2, 2500);

            ShowAccounts(bank, owner1);
            Console.WriteLine();

            ShowAccounts(bank, owner2);
            Console.WriteLine();

            Console.WriteLine("Money transfers!");
            bankManager.TransferMoney(bank, 500, account2, account1);
            bankManager.TransferMoney(bank, 500, account3, account1);

            ShowAccounts(bank);
            Console.WriteLine();

            Console.WriteLine("Account transfer!");
            bankManager.TransferAccount(bank, account2, owner2);

            ShowAccounts(bank, owner1);
            Console.WriteLine();
            
            ShowAccounts(bank, owner2);
            Console.WriteLine();

            Console.WriteLine("Finish!");
            Console.ReadKey();
        }

        private static void ShowAccounts(Bank bank)
            => bank.Accounts.ForEach(a => ShowAccount(a));

        private static void ShowAccount(Account account) 
            => Console.WriteLine($"{account.Id} => {account.Money} $");

        private static void ShowAccounts(Bank bank, Owner owner)
        {
            Console.WriteLine($"Accounts of {owner.FullName}:");
            bank.Accounts.Where(a => a.Owner == owner).ToList().ForEach(a => ShowAccount(a));
        }

        private static void PresentOwner(Owner owner) => Console.WriteLine($"New owner created => {owner.FullName}");
    }

    static class JsonExtension
    {
        public static string ToJsonString(this object obj) => JsonConvert.SerializeObject(obj);

        public static void Print(this object obj) => Console.WriteLine(obj.ToJsonString());
    }
}
