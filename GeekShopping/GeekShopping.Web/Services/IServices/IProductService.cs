using GeekShopping.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekShopping.Web.Services.IServices
{
    public interface IProductService
    {
        Task<IEnumerable<ProductModel>> FindAllProducts();
        Task<ProductModel> FindProductsById(long id);
        Task<ProductModel> CreateProduct(ProductModel productVO);
        Task<ProductModel> UpdateProduct(ProductModel productVO);
        Task<bool> DeleteProduct(long id);
    }
}
