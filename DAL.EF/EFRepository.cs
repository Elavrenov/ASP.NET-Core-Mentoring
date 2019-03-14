using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.CoreEntities.Entities;
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

            return Mapper.Mapper.ToCategoryDto(category);
        }
    }
}