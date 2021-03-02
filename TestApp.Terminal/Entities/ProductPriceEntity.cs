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
    }
}
