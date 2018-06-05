using System;
using System.Collections.Generic;
using System.Linq;

namespace BadBankApp.Models
{
    public class Bank
    {
        private static Bank _instance;
        public static Bank Instance
        {
            get 
            { 
                _instance = _instance ?? new Bank("BadBank");
                return _instance;
            }
        }

        private Bank(string name, params Account[] accounts)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Accounts = accounts?.ToList() ?? new List<Account>();
        }

        public string Name { get; set; }
        public List<Account> Accounts { get; }
    }
}