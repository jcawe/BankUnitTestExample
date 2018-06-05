using System;
using GoodBankApp.Factories;
using GoodBankApp.Managers;
using GoodBankApp.Models;
using Moq;
using NUnit.Framework;

namespace GoodBankApp.Test.Managers
{
    public class AccountManagerTest
    {
        private Mock<AccountFactory> mockAccountFactory;
        private AccountManager manager;

        [SetUp]
        public void Setup()
        {
            mockAccountFactory = new Mock<AccountFactory>();
            manager = new AccountManager(mockAccountFactory.Object);
        }

        private Account CreateStubAccount(decimal money, string firstName, string lastName)
             => new Account(Guid.NewGuid(), money, new Owner(Guid.NewGuid(), firstName, lastName));

        #region Withdraw
        [Test]
        public void WithdrawWithNegativeMoney()
        {
            //Given
            var money = -50;

            //When
            Assert.Throws<ArgumentException>(
                () => manager.Withdraw(null, money),
                AccountManager.MoneyNegativeError
            );
        }

        [Test]
        public void WithdrawWithAccountNull()
        {
            //When
            Assert.Throws<ArgumentNullException>(
                () => manager.Withdraw(null, 0),
                "account"
            );
        }

        [Test]
        public void WithdrawWithInsuficientFunds()
        {
            //Given
            var account = CreateStubAccount(50, "John", "Doe");
            var money = 100;

            //When
            Assert.Throws<InvalidOperationException>(
                () => manager.Withdraw(account, money),
                AccountManager.InsufficientFundsError
            );
        }

        [Test]
        public void WithdrawGoodCase()
        {
            //Given
            var account = CreateStubAccount(500, "John", "Doe");
            var money = 100;

            //When
            manager.Withdraw(account, money);

            //Then
            Assert.AreEqual(400, account.Money);
        }
        #endregion

        #region Deposit
        public void DepositWithNegativeMoney()
        {
            //Given
            var money = -50;

            //When
            Assert.Throws<ArgumentException>(
                () => manager.Deposit(null, money),
                AccountManager.MoneyNegativeError
            );
        }

        [Test]
        public void DepositWithAccountNull()
        {
            //When
            Assert.Throws<ArgumentNullException>(
                () => manager.Deposit(null, 0),
                "account"
            );
        }

        [Test]
        public void DepositGoodCase()
        {
            //Given
            var account = CreateStubAccount(500, "John", "Doe");
            var money = 100;

            //When
            manager.Deposit(account, money);

            //Then
            Assert.AreEqual(600, account.Money);
        }
        #endregion

        #region OpenAccount
        [Test]
        public void OpenAccountTest()
        {
            // Given
            var owner = new Owner(Guid.NewGuid(), "John", "Doe");
            var money = 50;
            var account = new Account(Guid.NewGuid(), money, owner);

            mockAccountFactory
                .Setup(f => f.Create(It.IsAny<Guid>(), money, owner))
                .Returns(account);

            // When
            var result = manager.OpenAccount(owner, money);

            // Then
            mockAccountFactory.Verify(
                f => f.Create(It.IsAny<Guid>(), money, owner),
                Times.Once()
            );

            Assert.AreEqual(account.Id, result.Id);
            Assert.AreEqual(account.Money, result.Money);
            Assert.AreEqual(account.Owner, result.Owner);
        }
        #endregion

        #region ChangeOwner
        [Test]
        public void ChangeOwnerWithNullAccount()
        {
            // When
            Assert.Throws<ArgumentNullException>(
                () => manager.ChangeOwner(null, null),
                "account"
            );
        }

        [Test]
        public void ChangeOwnerWithNullOwner()
        {
            // Given
            var account = CreateStubAccount(0, "John", "Doe");

            // When
            Assert.Throws<ArgumentNullException>(
                () => manager.ChangeOwner(account, null),
                "owner"
            );

        }

        [Test]
        public void ChangeOwnerWithSameOwner()
        {
            // Given
            var owner = new Owner(Guid.NewGuid(), "John", "Doe");
            var account = new Account(Guid.NewGuid(), 0, owner);

            // When
            Assert.Throws<InvalidOperationException>(
                () => manager.ChangeOwner(account, owner),
                string.Format(AccountManager.SameOwnerError, account.Id, owner.FullName)
            );
        }

        [Test]
        public void ChangeOwnerGoodCase()
        {
            // Given
            var account = CreateStubAccount(0, "John", "Doe");
            var owner = new Owner(Guid.NewGuid(), "Jane", "Doe");

            // When
            manager.ChangeOwner(account, owner);

            // Then
            Assert.AreEqual(owner, account.Owner);
        }
        #endregion
    }
}