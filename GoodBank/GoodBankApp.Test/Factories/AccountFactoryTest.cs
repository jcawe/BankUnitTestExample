using System;
using System.Collections.Generic;
using GoodBankApp.Factories;
using GoodBankApp.Models;
using NUnit.Framework;

namespace GoodBankApp.Test.Factories
{
    public class AccountFactoryTest
    {
        AccountFactory factory;

        [SetUp]
        public void Setup()
        {
            factory = new AccountFactory();
        }

        [Test]
        public void CreateTest()
        {
            // Given
            var id = Guid.NewGuid();
            var money = 500;
            var owner = new Owner(Guid.NewGuid(), "John", "Doe");

            // When
            var result = factory.Create(id, money, owner);

            // Then
            Assert.AreEqual(id, result.Id);
            Assert.AreEqual(money, result.Money);
            Assert.AreEqual(owner, result.Owner);
        }
    }
}