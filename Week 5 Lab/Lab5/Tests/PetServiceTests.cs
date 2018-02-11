﻿using FakeItEasy;
using Lab5.Data.Entities;
using Lab5.Repository;
using Lab5.Service;
using NUnit.Framework;
using System;

namespace Tests
{
    [TestFixture]
    public class PetServiceTests
    {
        private IPetRepository _repository;

        [SetUp]
        public void Setup()
        {
            _repository = A.Fake<IPetRepository>();
        }

        [Test]
        public void CheckupAlert_NoAlert()
        {
            // Arrange
            A.CallTo(() => _repository.GetPet(A<int>.Ignored)).Returns(new Pet
            {
                NextCheckup = DateTime.Today.AddDays(15)
            });

            // Act
            var service = new PetService(_repository);
            var view = service.GetPet(1);

            // Assert
            Assert.IsFalse(view.CheckupAlert);
        }

        [Test]
        public void CheckupAlert_YesAlert()
        {
            // Arrange
            A.CallTo(() => _repository.GetPet(A<int>.Ignored)).Returns(new Pet
            {
                NextCheckup = DateTime.Today.AddDays(14)
            });

            // Act
            var service = new PetService(_repository);
            var view = service.GetPet(1);

            // Assert
            Assert.IsTrue(view.CheckupAlert);
        }
    }
}