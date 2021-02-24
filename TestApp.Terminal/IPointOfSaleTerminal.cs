using System.Collections.Generic;
using TestApp.Terminal.Entities;

namespace TestApp.Terminal
{
    public interface IPointOfSaleTerminal
    {
        void SetPricing(IList<ProductPriceEntity> prices);

        void Scan(string productCode);

        decimal CalculateTotal();
    }
}
