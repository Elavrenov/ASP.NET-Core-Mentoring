using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.CoreEntities.Entities;
using BLL.CoreEntities.Entities.UpdateEntities;
using DAL.EF.Models;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF
{
    public class EfRepository : IRepository
    {
        private readonly NorthwindContext _context;

        public EfRepository(NorthwindContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            var allCategories = await _context.Categories.OrderBy(cat => cat.CategoryName).ToListAsync();
            return Mapper.Mapper.ToEnumerableCategoryDto(allCategories);
        }

        public async Task<Category> GetCategoryById(int? id)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.CategoryId == id);

            return category != null ? Mapper.Mapper.ToCategoryDto(category) : null;
        }

        public async Task CreateCategoryAsync(UpdateCategory newCategory)
        {
            await _context.Categories.AddAsync(Mapper.Mapper.ToCategoriesDal(newCategory));
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCateroryAsync(int id, UpdateCategory updatedCategory)
        {
            var existingCategory = await _context.Categories.SingleOrDefaultAsync(cat => cat.CategoryId == id);

            if (existingCategory != null)
            {
                existingCategory.CategoryName = updatedCategory.CategoryName;
                existingCategory.Description = updatedCategory.Description;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteCategoryAsync(int? id)
        {
            if (id != null)
            {
                var existingCategory = await _context.Categories.SingleOrDefaultAsync(cat => cat.CategoryId == id);

                if (existingCategory != null)
                {
                    _context.Categories.Remove(existingCategory);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}