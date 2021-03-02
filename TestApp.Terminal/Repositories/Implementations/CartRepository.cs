using System.Collections.Generic;
using System.Linq;
using TestApp.Terminal.Entities;
using TestApp.Terminal.Repositories.Interfaces;

namespace TestApp.Terminal.Repositories.Implementations
{
    public class CartRepository : ICartRepository
    {
        private IDictionary<string, ProductCartEntity> Products { get; }

        public CartRepository() => this.Products = new Dictionary<string, ProductCartEntity>();

        public IDictionary<string, ProductCartEntity> GetProducts() => this.Products;

        public void AddProduct(string productCode)
        {
            var product = this.GetProduct(productCode);

            if (product == null)
            {
                var newProduct = new ProductCartEntity(productCode);
                this.Products.Add(newProduct.ProductCode, newProduct);
                return;
            }

            product.Increase(1);
        }

        public void EmptyCart()
        {
            this.Products.Clear();
        }

        private ProductCartEntity GetProduct(string productCode)
        {
            return this.Products
                .Where(c => c.Key == productCode)
                .Select(p => p.Value)
                .FirstOrDefault();
        }
    }
}
