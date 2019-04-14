using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.CoreEntities.Entities;

namespace DAL.Interfaces.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int? id);
        Task<IEnumerable<string>> GetSelectedCategoryNames();
        Task<IEnumerable<string>> GetSelectedSupplierNames();
        Task CreateProductAsync(Product newProduct);
        Task UpdateProductAsync(int id, Product updatedProduct);
    }
}