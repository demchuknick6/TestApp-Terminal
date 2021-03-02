using System;
using System.Collections.Generic;
using System.Diagnostics;
using TestApp.Terminal.Entities;
using TestApp.Terminal.Repositories.Interfaces;
using TestApp.Terminal.Services.Interfaces;

namespace TestApp.Terminal.Services.Implementations
{
    public class PricingService : IPricingService
    {
        private readonly IPriceRepository _priceRepository;

        public PricingService(IPriceRepository priceRepository)
        {
            _priceRepository = priceRepository;
        }

        public void SetPricing(IList<ProductPriceEntity> prices)
        {
            foreach (var pricing in prices)
            {
                this.SetPricing(pricing);
            }
        }

        private void SetPricing(ProductPriceEntity pricing)
        {
            try
            {
                _priceRepository.SetProductPricing(pricing.ProductCode, pricing.UnitPrice, pricing.Volume, pricing.VolumePrice);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }
        }
    }
}
