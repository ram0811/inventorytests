using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace inventory.tests.DataModel
{
    public class ProductList : BaseResponse
    {
        public PaginationInfo Pagination { get; set; }
        public List<ProductItem> Items { get; set; }
    }

    public class ProductItem : BaseResponse
    {
        [JsonProperty("Guid")]
        public string ID { get; set; }
        public string ProductCode { get; set; }
        public string ProductDescription { get; set; }
        public UnitMeasure UnitOfMeasure { get; set; }
        [JsonProperty("ProductGroup")]
        public ProductGroup Group { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public bool IsSellable { get; set; }
    }

    public class UnitMeasureList : BaseResponse
    {
        public List<UnitMeasure> Items { get; set; }
    }

    public class UnitMeasure
    {
        [JsonProperty("Guid")]
        public string ID { get; set; }
        public string Name { get; set; }
    }

    public class ProductGroupList : BaseResponse
    {
        public List<ProductGroup> Items { get; set; }
    }

    public class ProductGroup
    {
        [JsonProperty("Guid")]
        public string ID { get; set; }
        public string GroupName { get; set; }
        public DateTime LastModifiedOn { get; set; }
    }
}
