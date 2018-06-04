using System;
using System.Collections.Generic;
using GoodBankApp.Factories;
using GoodBankApp.Models;
using NUnit.Framework;

namespace GoodBankApp.Test.Factories
{
    public class OwnerFactoryTest
    {
        OwnerFactory factory;

        [SetUp]
        public void Setup()
        {
            factory = new OwnerFactory();
        }

        [Test]
        public void CreateTest()
        {
            // Given
            var id = Guid.NewGuid();
            var firtName = "John";
            var lastName = "Doe";

            // When
            var result = factory.Create(id, firtName, lastName);

            // Then
            Assert.AreEqual(id, result.Id);
            Assert.AreEqual(firtName, result.FirstName);
            Assert.AreEqual(lastName, result.LastName);
        }
    }
}