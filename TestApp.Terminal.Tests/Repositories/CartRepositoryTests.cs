using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestApp.Terminal.Repositories.Implementations;
using TestApp.Terminal.Repositories.Interfaces;

namespace TestApp.Terminal.Tests.Repositories
{
    [TestClass]
    public class CartRepositoryTests
    {
        private ICartRepository _cartRepository;

        [TestInitialize]
        public void TestSetup()
        {
            _cartRepository = new CartRepository();
        }

        [DataTestMethod]
        [DataRow("A", 1)]
        [DataRow("A", 5)]
        public void AddProduct_GivenSameProductCodes_AreAddedToProducts(string productCode, int productCount)
        {
            for (int i = 0; i < productCount; i++)
            {
                _cartRepository.AddProduct(productCode);
            }

            var products = _cartRepository.GetProducts();
            Assert.AreEqual(1, products.Count);

            var productKeyValue = products.SingleOrDefault();
            Assert.AreEqual(productCode, productKeyValue.Key);
            Assert.AreEqual(productCode, productKeyValue.Value.ProductCode);
            Assert.AreEqual(productCount,productKeyValue.Value.Count);
        }

        [DataTestMethod]
        [DataRow(new[] { "A", "B", "C", "A" }, 3)]
        [DataRow(new[] { "A", "A", "A", "A" }, 1)]
        public void AddProduct_GivenDifferentProductCodes_AreAddedToProducts(string[] productCodes, int productsCount)
        {
            foreach (var productCode in productCodes)
            {
                _cartRepository.AddProduct(productCode);
            }

            var products = _cartRepository.GetProducts();

            Assert.AreEqual(productsCount, products.Count);

            foreach (var productCode in productCodes)
            {
                Assert.IsTrue(products.ContainsKey(productCode));
                Assert.AreEqual(productCode, products[productCode].ProductCode);
            }
        }

        [TestMethod]
        public void EmptyCart_ClearsProducts()
        {
            _cartRepository.AddProduct("A");

            Assert.AreEqual(1, _cartRepository.GetProducts().Count);
            
            _cartRepository.EmptyCart();

            Assert.IsFalse(_cartRepository.GetProducts().Any());
        }
    }
}
