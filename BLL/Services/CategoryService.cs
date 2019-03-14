using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.CoreEntities.Entities;
using BLL.Interfaces.Interfaces;
using DAL.Interfaces;

namespace BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository _repository;
        public CategoryService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _repository.GetAllCategoriesAsync();
        }

        public async Task<Category> GetCategoryById(int? id)
        {
            return await _repository.GetCategoryById(id);
        }
    }
}
