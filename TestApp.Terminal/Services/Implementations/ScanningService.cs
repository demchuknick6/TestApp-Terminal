using System;
using System.Diagnostics;
using TestApp.Terminal.Repositories.Interfaces;
using TestApp.Terminal.Services.Interfaces;

namespace TestApp.Terminal.Services.Implementations
{
    public class ScanningService : IScanningService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IPriceRepository _priceRepository;

        public ScanningService(ICartRepository cartRepository, IPriceRepository priceRepository)
        {
            _cartRepository = cartRepository;
            _priceRepository = priceRepository;
        }

        public void Scan(string productCode)
        {
            try
            {
                var productPricing = _priceRepository.FindProductPrice(productCode);

                if (productPricing != null)
                {
                    _cartRepository.AddProduct(productCode);
                }
                else
                {
                    Trace.WriteLine($"Product {productCode} has no price and cannot be scanned.");
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }
        }
    }
}
