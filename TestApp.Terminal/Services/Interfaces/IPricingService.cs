using System.Collections.Generic;
using TestApp.Terminal.Entities;

namespace TestApp.Terminal.Services.Interfaces
{
    public interface IPricingService : IService
    {
        void SetPricing(IList<ProductPriceEntity> prices);
    }
}
