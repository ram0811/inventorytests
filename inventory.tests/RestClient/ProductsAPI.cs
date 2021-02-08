using System;
using System.Threading.Tasks;
using inventory.tests.DataModel;

namespace inventory.tests.RestClient
{
    public class ProductsAPI : BaseClient
    {
        public ProductsAPI() : base()
        {
        }

        public Task<ProductList> GetProducts()
        {
            return Task.Run(() =>
            {
                var list = Get<ProductList>(new RequestModel("/Products"));
                return list;
            });
        }

        public Task<ProductItem> PostProduct(ProductPayload body)
        {
            return Task.Run(() =>
            {
                var model = new RequestModel("/Products/" + body.ID)
                {
                    Body = body,
                    IgnoreNullField = true
                };
                var item = Post<ProductItem>(model);
                return item;
            });
        }

        public Task<UnitMeasureList> GetUnitMeasures()
        {
            return Task.Run(() =>
            {
                var list = Get<UnitMeasureList>(new RequestModel("/UnitOfMeasures"));
                return list;
            });
        }

        public Task<ProductGroupList> GetProductGroups()
        {
            return Task.Run(() =>
            {
                var list = Get<ProductGroupList>(new RequestModel("/ProductGroups"));
                return list;
            });
        }
    }
}
