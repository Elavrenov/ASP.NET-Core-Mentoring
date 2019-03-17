using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.CoreEntities.Entities;
using BLL.CoreEntities.Entities.UpdateEntities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DAL.Interfaces.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int? id);

        Task<IEnumerable<string>> GetSelectedCategoryNames();
        Task<IEnumerable<string>> GetSelectedSupplierNames();
        Task CreateProductAsync(UpdateProduct newProduct);
        Task UpdateProductAsync(int id, UpdateProduct updatedProduct);
    }
}