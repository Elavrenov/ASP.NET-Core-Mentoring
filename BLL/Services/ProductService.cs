using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.CoreEntities.Entities;
using BLL.CoreEntities.Entities.UpdateEntities;
using BLL.Interfaces.Interfaces;
using DAL.Interfaces.Interfaces;

namespace BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _repository.GetAllProductsAsync();
        }

        public async Task<Product> GetProductByIdAsync(int? id)
        {
            return await _repository.GetProductByIdAsync(id);
        }

        public async Task<IEnumerable<string>> GetSelectedCategoryNames()
        {
            return await _repository.GetSelectedCategoryNames();
        }

        public async Task<IEnumerable<string>> GetSelectedSupplierNames()
        {
            return await _repository.GetSelectedSupplierNames();
        }

        public async Task CreateProductAsync(UpdateProduct newProduct)
        {
            await _repository.CreateProductAsync(newProduct);
        }

        public async Task UpdateProductAsync(int id, UpdateProduct updatedProduct)
        {
            await _repository.UpdateProductAsync(id, updatedProduct);
        }
    }
}