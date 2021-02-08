using System;
using Newtonsoft.Json;

namespace inventory.tests.DataModel
{
    public class ProductPayload
    {
        [JsonProperty("Guid")]
        public string ID { get; set; }
        public string ProductCode { get; set; }
        public string ProductDescription { get; set; }
        public UnitMeasure UnitOfMeasure { get; set; }
        [JsonProperty("ProductGroup")]
        public ProductGroup Group { get; set; }

        public ProductPayload(string guid = null)
        {
            ID = (guid ?? Guid.NewGuid().ToString("D"));
            UnitOfMeasure = new UnitMeasure();
            Group = new ProductGroup();
        }
    }
}
