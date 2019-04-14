using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.CoreEntities.Entities;

namespace DAL.Interfaces.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category> GetCategoryById(int? id);
        Task CreateCategoryAsync(Category newCategory);
        Task UpdateCateroryAsync(int id, Category updatedCategory);
    }
}