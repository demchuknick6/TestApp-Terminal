using System.Collections.Generic;
using System.Linq;
using TestApp.Terminal.Entities;

namespace TestApp.Terminal.Repositories
{
    internal class PriceRepository
    {
        public IDictionary<string, ProductPriceEntity> Prices { get; protected set; }

        public PriceRepository()
        {
            this.Prices = new Dictionary<string, ProductPriceEntity>();
        }

        public ProductPriceEntity GetProductPricing(string productCode)
        {
            return this.Prices
                .Where(c => c.Key == productCode)
                .Select(p => p.Value)
                .FirstOrDefault();
        }

        public void SetProductPricing(string productCode, decimal unitPrice,
            int? volume = null, decimal? volumePrice = null)
        {
            var productPricing = GetProductPricing(productCode);

            if (productPricing == null)
            {
                var newPricing = new ProductPriceEntity(productCode, unitPrice, volume, volumePrice);
                this.Prices.Add(productCode, newPricing);
                return;
            }

            productPricing.UnitPrice = unitPrice;
            productPricing.Volume = volume;
            productPricing.VolumePrice = volumePrice;
        }
    }
}
