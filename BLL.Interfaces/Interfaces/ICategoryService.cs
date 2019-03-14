using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.CoreEntities.Entities;

namespace BLL.Interfaces.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category> GetCategoryById(int? id);
    }
}
