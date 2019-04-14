using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.CoreEntities.Entities;

namespace BLL.Interfaces.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(int? id);
        Task CreateCategoryAsync(Category newCategory);
        Task UpdateCategoryAsync(int id, Category updatedCategory);
        Task<byte[]> GetPictureByCategoryId(int? id);
    }
}