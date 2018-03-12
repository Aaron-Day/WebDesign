using FakeItEasy;
using Lab9.Data.Entities;
using Lab9.Repository;
using Lab9.Service;
using NUnit.Framework;
using System;

namespace Tests
{
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
