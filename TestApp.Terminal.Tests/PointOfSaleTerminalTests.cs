using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestApp.Terminal.Entities;
using TestApp.Terminal.Repositories.Implementations;
using TestApp.Terminal.Repositories.Interfaces;
using TestApp.Terminal.Services.Implementations;
using TestApp.Terminal.Services.Interfaces;

namespace TestApp.Terminal.Tests
{
    [TestClass]
    public class PointOfSaleTerminalTests
    {
        private ICartRepository _cartRepository;
        private IPriceRepository _priceRepository;

        private IPricingService _pricingService;
        private IScanningService _scanningService;
        private ICalculationService _calculationService;
        
        private IPointOfSaleTerminal _terminal;

        //Imitate integration tests with all implemented repos and services 
        [TestInitialize]
        public void TestSetup()
        {
            _cartRepository = new CartRepository();
            _priceRepository = new PriceRepository();

            _pricingService = new PricingService(_priceRepository);
            _scanningService = new ScanningService(_cartRepository, _priceRepository);
            _calculationService = new CalculationService(_cartRepository, _priceRepository);
            
            _terminal = new PointOfSaleTerminal(_pricingService, _scanningService, _calculationService);
        }

        //Given test cases in the task and some custom ones
        [DataTestMethod]
        [DataRow(new string[] { }, 0)]
        [DataRow(new string[] { "A", "B", "C", "D", "A", "B", "A" }, 13.25)]
        [DataRow(new string[] { "C", "C", "C", "C", "C", "C", "C" }, 6)]
        [DataRow(new string[] { "A", "B", "C", "D" }, 7.25)]
        [DataRow(new string[] { "D", "C", "A", "B", "A", "B", "A" }, 13.25)]
        [DataRow(new string[] { "D", "C", "B", "A" }, 7.25)]
        [DataRow(new string[] { "C", "C", "C", "C", "C", "C", "D" }, 5.75)]
        [DataRow(new string[] { "C", "C", "A", "C", "C", "A", "C", "C", "A" }, 8)]
        [DataRow(new string[] { "A", "B", "C", "D", "X", "Y", "Z" }, 7.25)]
        public void CalculateTotal_GivenTestCases_ReturnsCorrectResults(string[] productCodes, double expectedValue)
        {
            _terminal.SetPricing(GetTestPrices());

            foreach (var productCode in productCodes)
            {
                _terminal.Scan(productCode);
            }

            var actualValue = _terminal.CalculateTotal();

            Assert.AreEqual((decimal)expectedValue, actualValue);
        }

        [TestMethod]
        public void Scan_GivenNullAsProductCode_ThrowsArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _terminal.Scan(null));
        }

        [TestMethod]
        public void Scan_GivenEmptyProductCode_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() => _terminal.Scan(string.Empty));
        }

        [TestMethod]
        public void SetPricing_GivenNullAsPricesList_ThrowsArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _terminal.SetPricing(null));
        }

        private static ProductPriceEntity[] GetTestPrices()
        {
            return new[]
            {
                new ProductPriceEntity("A", 1.25m, 3, 3.00m),
                new ProductPriceEntity("B", 4.25m),
                new ProductPriceEntity("C", 1.00m, 6, 5.00m),
                new ProductPriceEntity("D", 0.75m),
            };
        }
    }
}
