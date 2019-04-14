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
    public class EfCategoryRepository : ICategoryRepository
    {
        private const int DefaultByteMaskNumber = 78;
        private readonly NorthwindContext _context;

        public EfCategoryRepository(NorthwindContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            var allCategories = await _context.Categories.Include(c => c.Products).OrderBy(cat => cat.CategoryName)
                .ToListAsync();
            return Mapper.Mapper.ToEnumerableCategoryDto(allCategories);
        }

        public async Task<Category> GetCategoryById(int? id)
        {
            var category = await _context.Categories.Include(c => c.Products)
                .FirstOrDefaultAsync(m => m.CategoryId == id);

            return category != null ? Mapper.Mapper.ToCategoryDto(category) : null;
        }

        public async Task CreateCategoryAsync(Category newCategory)
        {
            var category = Mapper.Mapper.ToCategoriesDal(newCategory);

            if (!IsExistedCategory(category).Result)
            {
                await _context.Categories.AddAsync(category);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateCateroryAsync(int id, Category updatedCategory)
        {
            var existingCategory = await _context.Categories.SingleOrDefaultAsync(cat => cat.CategoryId == id);

            if (existingCategory == null)
            {
                throw new ArgumentException($"Cant update this item");
            }

            if (!string.Equals(existingCategory.CategoryName, updatedCategory.CategoryName, StringComparison.OrdinalIgnoreCase)
                || !string.Equals(existingCategory.Description, updatedCategory.Description, StringComparison.OrdinalIgnoreCase)
                || !IsEqualsPictures(existingCategory.Picture, updatedCategory.Picture))
            {
                existingCategory.CategoryName = updatedCategory.CategoryName;
                existingCategory.Description = updatedCategory.Description;
                existingCategory.Picture = updatedCategory.Picture;
            }

            await _context.SaveChangesAsync();
        }
        private bool IsEqualsPictures(byte[] dBytes, byte[] plBytes)
        {
            if (dBytes == null || plBytes == null)
            {
                return false;
            }

            return dBytes.Skip(DefaultByteMaskNumber).Count() == plBytes.Length;
        }
        private async Task<bool> IsExistedCategory(Categories category)
        {
            var categoryChecker = await _context.Categories.FirstOrDefaultAsync(x =>
                string.Equals(x.CategoryName, category.CategoryName, StringComparison.OrdinalIgnoreCase));

            if (categoryChecker == null)
            {
                return false;
            }

            throw new ArgumentException($"This item is already exist");
        }
    }
}