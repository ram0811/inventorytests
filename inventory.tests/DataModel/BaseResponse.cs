using Newtonsoft.Json;
using System;

namespace inventory.tests.DataModel
{
    public class BaseResponse
    {
        [JsonProperty("statusCode")]
        public string StatusCode { get; set; }

        [JsonProperty("statusDescription")]
        public string StatusDescription { get; set; }
    }

    public class PaginationInfo
    {
        public int NumberOfItems { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int NumberOfPages { get; set; }
    }
}
