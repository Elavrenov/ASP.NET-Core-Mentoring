using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.CoreEntities.Entities;
using BLL.CoreEntities.Entities.UpdateEntities;
using DAL.EF.Models;
using DAL.Interfaces.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            var products = await _context.Products.Include(p=>p.Category).Include(p=>p.Supplier).ToListAsync();
            return Mapper.Mapper.ToEnumerableProductDto(products);

        }

        public async Task<Product> GetProductByIdAsync(int? id)
        {
            var product = await _context.Products.Include(p => p.Category).Include(p => p.Supplier).FirstOrDefaultAsync(x => x.ProductId == id);
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

        public async Task CreateProductAsync(UpdateProduct newProduct)
        {
            await _context.Products.AddAsync(Mapper.Mapper.ToProductsDal(newProduct));
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(int id, UpdateProduct updatedProduct)
        {
            var existingProduct = await _context.Products.SingleOrDefaultAsync(p => p.ProductId == id);

            if (existingProduct != null)
            {
                existingProduct.Category = new Categories() { CategoryName = updatedProduct.CategoryIdNames };
                existingProduct.Discontinued = updatedProduct.Discontinued;
                existingProduct.QuantityPerUnit = updatedProduct.QuantityPerUnit;
                existingProduct.ReorderLevel = updatedProduct.ReorderLevel;
                existingProduct.Supplier = new Suppliers() { CompanyName = updatedProduct.SupplierIdNames };
                existingProduct.UnitPrice = updatedProduct.UnitPrice;
                existingProduct.UnitsInStock = updatedProduct.UnitsInStock;
                existingProduct.UnitsOnOrder = updatedProduct.UnitsOnOrder;
                existingProduct.ProductName = updatedProduct.ProductName;

                await _context.SaveChangesAsync();
            }
        }

    }
}