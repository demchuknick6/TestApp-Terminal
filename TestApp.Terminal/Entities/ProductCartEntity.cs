namespace TestApp.Terminal.Entities
{
    public class ProductCartEntity
    {
        public string ProductCode { get; }
        
        public int Count { get; set; }

        public ProductCartEntity(string productCode)
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
