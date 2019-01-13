using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Client.Models;
using Client.Services.Interfaces;
using Client.SQLite;
using Common;

namespace Client.Services.Mock
{
    public class ProductService : BaseSQLiteService<Models.Product>, IProductService
    {
        public ProductService(SQLiteDbContext sqliteDbContext) : base(sqliteDbContext)
        {
        }

        public Task<HttpResult<Product>> GetProductByBarcode(string barcode, CancellationToken token = default(CancellationToken))
        {
            var result = new HttpResult<Product>();
            result.Data = new Product();

            return Task.FromResult(result);
        }

        public Task<HttpResult<List<Product>>> GetProducts(FilterCriteria criteria, CancellationToken token = default(CancellationToken))
        {
            var result = new HttpResult<List<Product>>();
            result.Data = new List<Product>();

            return Task.FromResult(result);
        }

        public Task<HttpResult<bool>> UpdateProduct(Product entity, CancellationToken token = default(CancellationToken))
        {
            var result = new HttpResult<bool>();
            result.Data = true;

            return Task.FromResult(result);
        }
    }
}