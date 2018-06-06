using System;
using BadBankApp.Managers;
using BadBankApp.Models;
using NUnit.Framework;

namespace BadBankApp.Test.Managers
{
    public class BankManagerTest
    {
        private BankManager manager;

        [SetUp]
        public void Setup()
        {
            manager = new BankManager();
        }

        [Test]
        [TestCase(50)]
        public void OpenAccountTest(decimal money)
        {
            // Given
            var owner = new Owner(Guid.NewGuid(), "John", "Doe");

            // When
            manager.OpenAccount(owner, money);

            Assert.AreEqual(1, Bank.Instance.Accounts.Count);
            Assert.AreEqual(money, Bank.Instance.Accounts[0].Money);
        }
    }
}