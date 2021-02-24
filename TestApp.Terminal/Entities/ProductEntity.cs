namespace TestApp.Terminal.Entities
{
    internal class ProductEntity
    {
        public string ProductCode { get; }
        
        public int Count { get; set; }

        public ProductEntity(string productCode)
        {
            this.ProductCode = productCode;
            this.Count = 1;
        }

        public void Increase(int amount)
        {
            this.Count += amount;
        }
    }
}
