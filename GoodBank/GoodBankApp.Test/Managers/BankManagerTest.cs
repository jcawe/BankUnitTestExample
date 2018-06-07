using System;
using GoodBankApp.Factories;
using GoodBankApp.Managers;
using GoodBankApp.Models;
using Moq;
using NUnit.Framework;

namespace GoodBankApp.Test.Managers
{
    public class BankManagerTest
    {
        private Mock<IBankFactory> mockBankFactory;
        private Mock<IAccountManager> mockAccountManager;
        private BankManager manager;

        [SetUp]
        public void Setup()
        {
            mockBankFactory = new Mock<IBankFactory>();
            mockAccountManager = new Mock<IAccountManager>();
            manager = new BankManager(mockBankFactory.Object, mockAccountManager.Object);
        }

        private Account CreateStubAccount(decimal money, string firstName, string lastName, Guid id)
             => new Account(id, money, new Owner(Guid.NewGuid(), firstName, lastName));
        
        private Account CreateStubAccount(decimal money, string firstName, string lastName)
            => CreateStubAccount(money, firstName, lastName, Guid.NewGuid());

        private Bank CreateStubBank(string name, params Account[] accounts)
            => new Bank(name, accounts);

        #region OpenBank
        [Test]
        public void OpenBankTest()
        {
            // Given
            var name = "GoodBankTest";
            var account1 = CreateStubAccount(0, "John", "Doe");
            var account2 = CreateStubAccount(0, "Jane", "Doe");
            var bank = CreateStubBank(name, account1, account2);

            mockBankFactory
                .Setup(f => f.Create(name, account1, account2))
                .Returns(bank);

            // When
            var result = manager.OpenBank(name, account1, account2);

            // Then
            mockBankFactory
            .Verify(
                f => f.Create(name, account1, account2),
                Times.Once()
            );

            Assert.AreEqual(bank, result);
        }
        #endregion
        #region TransferMoney
        [Test]
        public void TransferMoneyWithNullBank()
        {
            // When
            Assert.Throws<ArgumentNullException>(
                () => manager.TransferMoney(null, 0, Guid.Empty, Guid.Empty),
                "bank"
            );
        }

        [Test]
        [TestCase("de9847ff-bd1b-477e-abcb-02550f58cd8f", "f53cfc25-5e30-4775-b12f-a12f82d905c6")]
        [TestCase("f53cfc25-5e30-4775-b12f-a12f82d905c6", "de9847ff-bd1b-477e-abcb-02550f58cd8f")]
        public void TransferMoneyWithAccountNotInBank(string sourceIdStr, string targetIdStr)
        {
            // Given
            var sourceId = Guid.Parse(sourceIdStr);
            var targetId = Guid.Parse(targetIdStr);

            var foundId = Guid.Parse("f53cfc25-5e30-4775-b12f-a12f82d905c6");
            var notFoundId = Guid.Parse("de9847ff-bd1b-477e-abcb-02550f58cd8f");
            var account = CreateStubAccount(0, "John", "Doe", foundId);
            var bank = CreateStubBank("GoodBankTest", account);

            // When
            Assert.Throws<Exception>(
                () => manager.TransferMoney(bank, 0, sourceId, targetId),
                string.Format(BankManager.AccountNotFoundError, notFoundId)
            );
        }

        [Test]
        public void TransferMoneyGoodCase()
        {
            // Given
            var account1 = CreateStubAccount(0, "John", "Doe");
            var account2 = CreateStubAccount(0, "Jane", "Doe");
            var bank = CreateStubBank("GoodBankTest", account1, account2);
            var money = 50;

            mockAccountManager.Setup(m => m.Withdraw(account1, money));
            mockAccountManager.Setup(m => m.Deposit(account2, money));

            // When
            manager.TransferMoney(bank, money, account1.Id, account2.Id);

            // Then
            mockAccountManager.Verify(m => m.Withdraw(account1, money), Times.Once());
            mockAccountManager.Verify(m => m.Deposit(account2, money), Times.Once());
        }
        #endregion
        #region OpenAccount
        [Test]
        public void OpenAccountTest()
        {
            // Given
            var money = 50;
            var owner = new Owner(Guid.Empty, "John", "Doe");
            var account = CreateStubAccount(money, "John", "Doe");
            var bank = CreateStubBank("GoodBankTest");

            mockAccountManager
                .Setup(m => m.OpenAccount(owner, money))
                .Returns(account);

            // When
            var result = manager.OpenAccount(bank, owner, money);

            // Then
            mockAccountManager.Verify(
                m => m.OpenAccount(owner, money),
                Times.Once()
            );

            Assert.AreEqual(account, bank.Accounts[0]);
            Assert.AreEqual(account.Id, result);
        }
        #endregion
    }
}