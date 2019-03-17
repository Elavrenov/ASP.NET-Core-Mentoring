using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.CoreEntities.Entities;
using BLL.CoreEntities.Entities.UpdateEntities;

namespace DAL.Interfaces.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category> GetCategoryById(int? id);
        Task CreateCategoryAsync(UpdateCategory newCategory);
        Task UpdateCateroryAsync(int id, UpdateCategory updatedCategory);
    }
}
