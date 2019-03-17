using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.CoreEntities.Entities;
using BLL.CoreEntities.Entities.UpdateEntities;

namespace BLL.Interfaces.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int? id);
        Task<IEnumerable<string>> GetSelectedCategoryNames();
        Task<IEnumerable<string>> GetSelectedSupplierNames();
        Task CreateProductAsync(UpdateProduct newProduct);
        Task UpdateProductAsync(int id, UpdateProduct updatedProduct);
    }
}