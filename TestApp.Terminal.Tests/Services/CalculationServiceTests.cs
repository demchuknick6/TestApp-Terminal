using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using TestApp.Terminal.Entities;
using TestApp.Terminal.Repositories.Interfaces;
using TestApp.Terminal.Services.Implementations;
using TestApp.Terminal.Services.Interfaces;

namespace TestApp.Terminal.Tests.Services
{
    [TestClass]
    public class CalculationServiceTests
    {
        private ICartRepository _cartRepository;
        private IPriceRepository _priceRepository;
        private ICalculationService _calculationService;

        [TestInitialize]
        public void TestSetup()
        {
            _cartRepository = Substitute.For<ICartRepository>();
            _priceRepository = Substitute.For<IPriceRepository>();
            _calculationService = new CalculationService(_cartRepository, _priceRepository);
        }

        [DataTestMethod]
        [DataRow("A", "B", "C")]
        public void CalculateTotal_GivenThreeCartProducts_ReturnsCorrectlyCalculatedValue(string codeA,
            string codeB, string codeC)
        {
            _cartRepository.GetProducts().Returns(new Dictionary<string, ProductCartEntity>
            {
                {codeA, new ProductCartEntity(codeA)},
                {codeB, new ProductCartEntity(codeB)},
                {codeC, new ProductCartEntity(codeC)},
            });

            _priceRepository.CalculateProductPrice(codeA, 1).Returns(1);
            _priceRepository.CalculateProductPrice(codeB, 1).Returns(2);
            _priceRepository.CalculateProductPrice(codeC, 1).Returns(3);

            var expectedValue = 6m;
            var actualValue = _calculationService.CalculateTotal();

            Assert.AreEqual(expectedValue, actualValue);

            _cartRepository.Received(1).GetProducts();
            _priceRepository.ReceivedWithAnyArgs(3).CalculateProductPrice(string.Empty, 0);
        }
    }
}
