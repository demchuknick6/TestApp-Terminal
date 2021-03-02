using System.Collections.Generic;
using TestApp.Terminal.Entities;

namespace TestApp.Terminal.Repositories.Interfaces
{
    public interface ICartRepository
    {
        IDictionary<string, ProductCartEntity> GetProducts();

        void AddProduct(string productCode);

        void EmptyCart();
    }
}
