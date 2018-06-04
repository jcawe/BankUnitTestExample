using System;

namespace BadBankApp.Models
{
    public class Account
    {
        public Account(Guid id, decimal money, Owner owner)
        {
            Id = id;
            Money = money;
            Owner = owner ?? throw new ArgumentNullException(nameof(owner));
        }
        public Guid Id { get; }

        public decimal Money { get; set; }

        public Owner Owner { get; set; }
    }
}