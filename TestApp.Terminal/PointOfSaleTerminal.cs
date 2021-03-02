using System.Collections.Generic;
using TestApp.Terminal.Entities;
using TestApp.Terminal.Helpers;
using TestApp.Terminal.Services.Interfaces;

namespace TestApp.Terminal
{
    public class PointOfSaleTerminal : IPointOfSaleTerminal
    {
        private readonly IPricingService _pricingService;
        private readonly IScanningService _scanningService;
        private readonly ICalculationService _calculationService;

        public PointOfSaleTerminal(IPricingService pricingService, IScanningService scanningService,
            ICalculationService calculationService)
        {
            _pricingService = pricingService;
            _scanningService = scanningService;
            _calculationService = calculationService;
        }

        public void SetPricing(IList<ProductPriceEntity> prices)
        {
            GuardClausesHelper.IsNotNull(prices, nameof(prices));

            _pricingService.SetPricing(prices);
        }

        public void Scan(string productCode)
        {
            GuardClausesHelper.IsNotNullOrEmpty(productCode, nameof(productCode));

            _scanningService.Scan(productCode);
        }

        public decimal CalculateTotal()
        {
            return _calculationService.CalculateTotal();
        }
    }
}
