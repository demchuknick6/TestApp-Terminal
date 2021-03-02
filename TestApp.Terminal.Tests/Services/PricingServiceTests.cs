using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using TestApp.Terminal.Entities;
using TestApp.Terminal.Repositories.Interfaces;
using TestApp.Terminal.Services.Implementations;
using TestApp.Terminal.Services.Interfaces;

namespace TestApp.Terminal.Tests.Services
{
    [TestClass]
    public class PricingServiceTests
    {
        private IPriceRepository _priceRepository;
        private IPricingService _pricingService;

        [TestInitialize]
        public void TestSetup()
        {
            _priceRepository = Substitute.For<IPriceRepository>();
            _pricingService = new PricingService(_priceRepository);
        }

        [TestMethod]
        public void SetPricing_GivenProductPriceList_CallsSetProductPricingForEachPrice()
        {
            var prices = new[]
            {
                new ProductPriceEntity("A", 1.25m, 3, 3.00m),
                new ProductPriceEntity("B", 4.25m),
                new ProductPriceEntity("C", 1.00m, 6, 5.00m),
                new ProductPriceEntity("D", 0.75m),
            };

            _pricingService.SetPricing(prices);

            foreach (var price in prices)
            {
                _priceRepository.Received(1)
                    .SetProductPricing(price.ProductCode, price.UnitPrice, price.Volume, price.VolumePrice);
            }
        }

        [TestMethod]
        public void SetPricing_GivenEmptyProductPriceList_DoesNotCallSetProductPricing()
        {
            var prices = new ProductPriceEntity[] { };

            _pricingService.SetPricing(prices);

            _priceRepository.DidNotReceiveWithAnyArgs().SetProductPricing(string.Empty, 0m, null, null);
        }
    }
}
