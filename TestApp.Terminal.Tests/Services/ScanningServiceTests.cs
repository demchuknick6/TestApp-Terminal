using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using TestApp.Terminal.Entities;
using TestApp.Terminal.Repositories.Interfaces;
using TestApp.Terminal.Services.Implementations;
using TestApp.Terminal.Services.Interfaces;

namespace TestApp.Terminal.Tests.Services
{
    [TestClass]
    public class ScanningServiceTests
    {
        private ICartRepository _cartRepository;
        private IPriceRepository _priceRepository;
        private IScanningService _scanningService;
        private string _productCode;

        [TestInitialize]
        public void TestSetup()
        {
            _cartRepository = Substitute.For<ICartRepository>();
            _priceRepository = Substitute.For<IPriceRepository>();
            _scanningService = new ScanningService(_cartRepository, _priceRepository);
            _productCode = "A";
        }

        [TestMethod]
        public void Scan_GivenProductWithPrice_CallsAddProductMethod()
        {
            _priceRepository.FindProductPrice(_productCode).Returns(
                new ProductPriceEntity(_productCode, 1.25m, 3, 3.00m));

            _scanningService.Scan(_productCode);

            _cartRepository.Received(1).AddProduct(_productCode);
        }

        [TestMethod]
        public void Scan_GivenProductWithoutPrice_DoesNotCallAddProductMethod()
        {
            _priceRepository.FindProductPrice(_productCode).Returns((ProductPriceEntity)null);

            _scanningService.Scan(_productCode);

            _cartRepository.DidNotReceiveWithAnyArgs().AddProduct(_productCode);
        }
    }
}
