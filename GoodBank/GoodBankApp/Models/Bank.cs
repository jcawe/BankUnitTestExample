using System;
using System.Collections.Generic;
using System.Linq;

namespace GoodBankApp.Models
{
    public class Bank
    {
        public Bank(string name, params Account[] accounts)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Accounts = accounts?.ToList() ?? new List<Account>();
        }

        public string Name { get; set; }
        public List<Account> Accounts { get; }
    }
}