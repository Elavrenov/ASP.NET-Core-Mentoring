using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.CoreEntities.Entities;
using DAL.EF.Models;
using DAL.Interfaces.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Repositories
{
    public class EfProductRepository : IProductRepository
    {
        private readonly NorthwindContext _context;

        public EfProductRepository(NorthwindContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var products = await _context.Products.Include(p => p.Category).Include(p => p.Supplier).ToListAsync();
            return Mapper.Mapper.ToEnumerableProductDto(products);
        }

        public async Task<Product> GetProductByIdAsync(int? id)
        {
            var product = await _context.Products.Include(p => p.Category).Include(p => p.Supplier)
                .FirstOrDefaultAsync(x => x.ProductId == id);
            return product != null ? Mapper.Mapper.ToProductDto(product) : null;
        }

        public async Task<IEnumerable<string>> GetSelectedCategoryNames()
        {
            return await _context.Categories.Select(x => x.CategoryName).ToListAsync();
        }

        public async Task<IEnumerable<string>> GetSelectedSupplierNames()
        {
            return await _context.Suppliers.Select(x => x.CompanyName).ToListAsync();
        }
        public async Task CreateProductAsync(Product newProduct)
        {
            var product = Mapper.Mapper.ToProductsDal(newProduct, _context);

            if (!IsExistedProduct(product).Result)
            {
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateProductAsync(int id, Product updatedProduct)
        {
            var existingProduct = await _context.Products.SingleOrDefaultAsync(p => p.ProductId == id);

            if (existingProduct != null)
            {
                existingProduct.Category =
                    await _context.Categories.FirstAsync(x =>
                        string.Equals($"{x.CategoryName}", $"{updatedProduct.Category}", StringComparison.OrdinalIgnoreCase));
                existingProduct.Discontinued = updatedProduct.Discontinued;
                existingProduct.QuantityPerUnit = updatedProduct.QuantityPerUnit;
                existingProduct.ReorderLevel = updatedProduct.ReorderLevel;
                existingProduct.Supplier = await _context.Suppliers.FirstAsync(x =>
                    string.Equals($"{x.CompanyName}", $"{updatedProduct.Supplier}", StringComparison.OrdinalIgnoreCase));
                existingProduct.UnitPrice = updatedProduct.UnitPrice;
                existingProduct.UnitsInStock = updatedProduct.UnitsInStock;
                existingProduct.UnitsOnOrder = updatedProduct.UnitsOnOrder;
                existingProduct.ProductName = updatedProduct.ProductName;

                await _context.SaveChangesAsync();
            }
        }

        private async Task<bool> IsExistedProduct(Products product)
        {
            var productChecker = await _context.Products.FirstOrDefaultAsync(x =>
                string.Equals(x.ProductName, product.ProductName, StringComparison.OrdinalIgnoreCase));

            if (productChecker == null)
            {
                return false;
            }

            throw new ArgumentException($"This item is already exist");
        }
    }
}