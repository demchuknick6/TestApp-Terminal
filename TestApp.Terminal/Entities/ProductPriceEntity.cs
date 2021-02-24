namespace TestApp.Terminal.Entities
{
    public class ProductPriceEntity
    {
        public string ProductCode { get; set; }
        
        public decimal UnitPrice { get; set; }

        public int? Volume { get; set; }

        public decimal? VolumePrice { get; set; }

        public ProductPriceEntity(string productCode, decimal unitPrice,
            int? volume = null, decimal? volumePrice = null)
        {
            this.ProductCode = productCode;
            this.UnitPrice = unitPrice;
            this.Volume = volume;
            this.VolumePrice = volumePrice;
        }

        public decimal CalculateProductPrice(int productCount)
        {
            if (this.Volume == null || this.VolumePrice == null)
            {
                return this.UnitPrice * productCount;
            }

            var singlesCount = productCount % this.Volume;
            var volumeGroupsCount = (int)(productCount / this.Volume);

            return (decimal)(this.UnitPrice * singlesCount + this.VolumePrice * volumeGroupsCount);
        }
    }
}
