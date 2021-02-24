using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestApp.Terminal.Entities;

namespace TestApp.Terminal.Tests
{
    [TestClass]
    public class PointOfSaleTerminalTests
    {
        private IPointOfSaleTerminal _terminal;

        [TestInitialize]
        public void TestSetup()
        {
            _terminal = new PointOfSaleTerminal();

            var prices = new[]
            {
                new ProductPriceEntity("A", 1.25m, 3, 3.00m),
                new ProductPriceEntity("B", 4.25m),
                new ProductPriceEntity("C", 1.00m, 6, 5.00m),
                new ProductPriceEntity("D", 0.75m),
            };
            _terminal.SetPricing(prices);
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
        public void CalculateTotal_GivenTestCases_ReturnsCorrectResults(string[] productCodes, double expectedValue)
        {
            foreach (var productCode in productCodes)
            {
                _terminal.Scan(productCode);
            }

            var actualValue = _terminal.CalculateTotal();

            Assert.AreEqual((decimal)expectedValue, actualValue);
        }
    }
}
