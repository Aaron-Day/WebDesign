using BankLedger.Repositories;
using BankLedger.Services;
using FakeItEasy;
using NUnit.Framework;

namespace Test
{
    public class BankLedgerTests
    {
        private IBankRepository _repository;

        [SetUp]
        public void Setup()
        {
            _repository = A.Fake<IBankRepository>();
        }

        [Test]
        public void CheckupAlert_NoAlert()
        {
            // Arrange
            //A.CallTo(() => _repository.GetPet(A<int>.Ignored)).Returns(new Pet
            //{
            //    NextCheckup = DateTime.Today.AddDays(15)
            //});

            // Act
            var service = new BankService(_repository);
            //var view = service.GetPet(1);

            // Assert
            //Assert.IsFalse(view.CheckupAlert);
        }
    }
}
