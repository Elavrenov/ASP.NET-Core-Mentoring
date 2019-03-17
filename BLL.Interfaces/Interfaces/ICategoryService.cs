using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.CoreEntities.Entities;
using BLL.CoreEntities.Entities.UpdateEntities;

namespace BLL.Interfaces.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(int? id);
        Task CreateCategoryAsync(UpdateCategory newCategory);
        Task UpdateCategoryAsync(int id, UpdateCategory updatedCategory);
    }
}