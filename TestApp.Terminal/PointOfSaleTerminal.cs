using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TestApp.Terminal.Entities;
using TestApp.Terminal.Repositories;

namespace TestApp.Terminal
{
    public class PointOfSaleTerminal : IPointOfSaleTerminal
    {
        private readonly CartRepository _cart;
        private readonly PriceRepository _prices;
        
        public PointOfSaleTerminal()
        {
            _cart = new CartRepository();
            _prices = new PriceRepository();
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
                _prices.SetProductPricing(pricing.ProductCode, pricing.UnitPrice, pricing.Volume, pricing.VolumePrice);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }
        }

        public void Scan(string productCode)
        {
            try
            {
                var productPricing = _prices.GetProductPricing(productCode);

                if (productPricing != null)
                {
                    _cart.AddProduct(productCode);
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }
        }

        public decimal CalculateTotal()
        {
            return _cart.Products.Sum(product =>
                _prices.GetProductPricing(product.Key).CalculateProductPrice(product.Value.Count));
        }

        public void EmptyCart()
        {
            _cart.EmptyCart();
        }
    }
}
