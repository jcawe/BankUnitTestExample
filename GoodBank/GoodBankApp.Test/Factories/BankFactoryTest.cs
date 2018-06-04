using System;
using System.Collections.Generic;
using GoodBankApp.Factories;
using GoodBankApp.Models;
using NUnit.Framework;

namespace GoodBankApp.Test.Factories
{
    public class BankFactoryTest
    {
        BankFactory factory;

        [SetUp]
        public void Setup()
        {
            factory = new BankFactory();
        }

        [Test]
        public void CreateTest()
        {
            // Given
            var name = "FakeBank";
            var account1 = CreateStubAccount(500, "John", "Doe");
            var account2 = CreateStubAccount(1000, "Jane", "Doe");

            // When
            var result = factory.Create(name, account1, account2);

            // Then
            Assert.AreEqual(name, result.Name);
            Assert.NotNull(result.Accounts);
            Assert.AreEqual(2, result.Accounts.Count);
            Assert.AreEqual(account1, result.Accounts[0]);
            Assert.AreEqual(account2, result.Accounts[1]);
        }

        private Account CreateStubAccount(decimal money, string firstName, string lastName)
             => new Account(Guid.NewGuid(), money, new Owner(Guid.NewGuid(), firstName, lastName));
    }
}