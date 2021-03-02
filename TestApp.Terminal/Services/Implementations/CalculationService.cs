using System;
using System.Diagnostics;
using System.Linq;
using TestApp.Terminal.Repositories.Interfaces;
using TestApp.Terminal.Services.Interfaces;

namespace TestApp.Terminal.Services.Implementations
{
    public class CalculationService : ICalculationService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IPriceRepository _priceRepository;

        public CalculationService(ICartRepository cartRepository, IPriceRepository priceRepository)
        {
            _cartRepository = cartRepository;
            _priceRepository = priceRepository;
        }

        public decimal CalculateTotal()
        {
            var total = 0m;
            try
            {
                total = _cartRepository.GetProducts().Sum(product =>
                    _priceRepository.CalculateProductPrice(product.Key, product.Value.Count));
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }

            return total;
        }
    }
}
