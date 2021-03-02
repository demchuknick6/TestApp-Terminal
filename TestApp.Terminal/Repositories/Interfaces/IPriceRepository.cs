using System.Collections.Generic;
using TestApp.Terminal.Entities;

namespace TestApp.Terminal.Repositories.Interfaces
{
    public interface IPriceRepository
    {
        IDictionary<string, ProductPriceEntity> GetPrices();

        ProductPriceEntity FindProductPrice(string productCode);

        void SetProductPricing(string productCode, decimal unitPrice, int? volume, decimal? volumePrice);

        decimal CalculateProductPrice(string productCode, int productCount);
    }
}
