using System.Collections.Generic;
using System.Linq;
using TestApp.Terminal.Entities;

namespace TestApp.Terminal.Repositories
{
    internal class CartRepository
    {
        public IDictionary<string, ProductEntity> Products { get; protected set; }

        public CartRepository() => this.Products = new Dictionary<string, ProductEntity>();

        public void AddProduct(string productCode)
        {
            var product = this.GetProduct(productCode);

            if (product == null)
            {
                var newProduct = new ProductEntity(productCode);
                this.Products.Add(newProduct.ProductCode, newProduct);
                return;
            }

            product.Increase(1);
        }

        public void EmptyCart()
        {
            this.Products.Clear();
        }

        private ProductEntity GetProduct(string productCode)
        {
            return this.Products
                .Where(c => c.Key == productCode)
                .Select(p => p.Value)
                .FirstOrDefault();
        }
    }
}
