using System;
using System.Collections.Generic;
using System.Linq;
using TestApp.Terminal.Entities;
using TestApp.Terminal.Repositories.Interfaces;

namespace TestApp.Terminal.Repositories.Implementations
{
    public class PriceRepository : IPriceRepository
    {
        private IDictionary<string, ProductPriceEntity> Prices { get; }

        public PriceRepository()
        {
            this.Prices = new Dictionary<string, ProductPriceEntity>();
        }

        public IDictionary<string, ProductPriceEntity> GetPrices() => this.Prices;

        public ProductPriceEntity FindProductPrice(string productCode)
        {
            return this.Prices
                .Where(c => c.Key == productCode)
                .Select(p => p.Value)
                .FirstOrDefault();
        }

        public void SetProductPricing(string productCode, decimal unitPrice,
            int? volume = null, decimal? volumePrice = null)
        {
            var productPrice = FindProductPrice(productCode);

            if (productPrice == null)
            {
                var newPricing = new ProductPriceEntity(productCode, unitPrice, volume, volumePrice);
                this.Prices.Add(productCode, newPricing);
                return;
            }

            productPrice.UnitPrice = unitPrice;
            productPrice.Volume = volume;
            productPrice.VolumePrice = volumePrice;
        }

        public decimal CalculateProductPrice(string productCode, int productCount)
        {
            var productPrice = FindProductPrice(productCode) ??
                               throw new NullReferenceException($"Price for product {productCode} cannot be null.");

            var productVolume = productPrice.Volume;
            var productVolumePrice = productPrice.VolumePrice;

            if (productVolume == null || productVolumePrice == null)
            {
                return productPrice.UnitPrice * productCount;
            }

            var singlesCount = productCount % productVolume;
            var volumeGroupsCount = (int)(productCount / productVolume);

            return (decimal)(productPrice.UnitPrice * singlesCount + productVolumePrice * volumeGroupsCount);
        }
    }
}
