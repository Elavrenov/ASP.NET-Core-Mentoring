using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.CoreEntities.Entities;
using BLL.CoreEntities.Entities.UpdateEntities;
using DAL.Interfaces.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF
{
    public class EfCategoryRepository : ICategoryRepository
    {
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
    }
}