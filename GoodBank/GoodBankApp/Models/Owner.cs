using System;

namespace GoodBankApp.Models
{
    public class Owner
    {
        public Owner(Guid id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
        }

        public Guid Id { get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
    }
}