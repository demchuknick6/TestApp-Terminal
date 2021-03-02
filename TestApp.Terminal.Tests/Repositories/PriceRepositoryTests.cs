using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestApp.Terminal.Repositories.Implementations;
using TestApp.Terminal.Repositories.Interfaces;

namespace TestApp.Terminal.Tests.Repositories
{
    [TestClass]
    public class PriceRepositoryTests
    {
        private IPriceRepository _priceRepository;

        [TestInitialize]
        public void TestSetup()
        {
            _priceRepository = new PriceRepository();
        }

        [DataTestMethod]
        [DataRow("A", 3.0, null, null)]
        [DataRow("B", 3.0, null, 12.0)]
        [DataRow("C", 3.0, 5, null)]
        [DataRow("D", 3.0, 5, 12.0)]
        public void FindProductPrice_GivenProductCode_ReturnsCorrectProductPrice(string productCode, double unitPrice,
            int? volume, double? volumePrice)
        {
            var unitPriceDecimal = (decimal)unitPrice;
            var volumePriceDecimal = (decimal?)volumePrice;
            _priceRepository.SetProductPricing(productCode, unitPriceDecimal, volume, volumePriceDecimal);

            var productPrice = _priceRepository.FindProductPrice(productCode);
            
            Assert.AreEqual(productCode, productPrice.ProductCode);
            Assert.AreEqual(unitPriceDecimal, productPrice.UnitPrice);
            Assert.AreEqual(volume, productPrice.Volume);
            Assert.AreEqual(volumePriceDecimal, productPrice.VolumePrice);
        }

        [DataTestMethod]
        [DataRow("E")]
        [DataRow("G")]
        [DataRow("X")]
        public void FindProductPrice_GivenProductCodeThatHasNoPrice_ReturnsNull(string productCode)
        {
            var productPrice = _priceRepository.FindProductPrice(productCode);

            Assert.IsNull(productPrice);
        }

        [DataTestMethod]
        [DataRow("A", 3.0, null, null)]
        [DataRow("A", 3.0, null, 12.0)]
        [DataRow("A", 3.0, 5, null)]
        [DataRow("A", 3.0, 5, 12.0)]
        public void SetProductPricing_GivenNewProductCode_SetsPricing(string productCode, double unitPrice,
            int? volume, double? volumePrice)
        {
            var unitPriceDecimal = (decimal) unitPrice;
            var volumePriceDecimal = (decimal?) volumePrice;
            _priceRepository.SetProductPricing(productCode, unitPriceDecimal, volume, volumePriceDecimal);

            var productPriceKeyValue = _priceRepository.GetPrices().SingleOrDefault();

            Assert.AreEqual(productCode, productPriceKeyValue.Key);
            Assert.AreEqual(productCode, productPriceKeyValue.Value.ProductCode);
            Assert.AreEqual(unitPriceDecimal, productPriceKeyValue.Value.UnitPrice);
            Assert.AreEqual(volume, productPriceKeyValue.Value.Volume);
            Assert.AreEqual(volumePriceDecimal, productPriceKeyValue.Value.VolumePrice);
        }

        [DataTestMethod]
        [DataRow("A", 3.0, null, null)]
        [DataRow("A", 3.0, null, 12.0)]
        [DataRow("A", 3.0, 5, null)]
        [DataRow("A", 3.0, 5, 12.0)]
        public void SetProductPricing_GivenExistingProductCode_OverwritesExistingPricing(string productCode, double unitPrice,
            int? volume, double? volumePrice)
        {
            //setting initial pricing for 'A' product code
            _priceRepository.SetProductPricing(productCode, 1m, 3, 2.5m);

            var unitPriceDecimal = (decimal)unitPrice;
            var volumePriceDecimal = (decimal?)volumePrice;
            
            //overwriting initial pricing for 'A' product code with some test data
            _priceRepository.SetProductPricing(productCode, unitPriceDecimal, volume, volumePriceDecimal);

            var productPriceKeyValue = _priceRepository.GetPrices().SingleOrDefault();

            Assert.AreEqual(productCode, productPriceKeyValue.Key);
            Assert.AreEqual(productCode, productPriceKeyValue.Value.ProductCode);
            Assert.AreEqual(unitPriceDecimal, productPriceKeyValue.Value.UnitPrice);
            Assert.AreEqual(volume, productPriceKeyValue.Value.Volume);
            Assert.AreEqual(volumePriceDecimal, productPriceKeyValue.Value.VolumePrice);
        }

        [DataTestMethod]
        [DataRow("A", 1, 1.25)]
        [DataRow("A", 2, 2.50)]
        [DataRow("A", 3, 3.0)]
        [DataRow("A", 4, 4.25)]
        [DataRow("A", 9, 9.00)]
        public void CalculateProductPrice_GivenExistingProductCode_ReturnsCorrectResult(string productCode,
            int productCount, double expectedResult)
        {
            _priceRepository.SetProductPricing(productCode, 1.25m, 3, 3.00m);

            var actualResult = _priceRepository.CalculateProductPrice(productCode, productCount);

            Assert.AreEqual((decimal)expectedResult, actualResult);
        }

        [TestMethod]
        public void CalculateProductPrice_GivenNonexistentProductCode_ThrowsNullReferenceException()
        {
            var productCode = "Z";
            var exception = Assert.ThrowsException<NullReferenceException>(() => _priceRepository.CalculateProductPrice(productCode, 4));
            Assert.AreEqual($"Price for product {productCode} cannot be null.", exception.Message);
        }
    }
}
