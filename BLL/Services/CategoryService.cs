using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.CoreEntities.Entities;
using BLL.CoreEntities.Entities.UpdateEntities;
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

        public async Task<Category> GetCategoryByIdAsync(int? id)
        {
            return await _repository.GetCategoryById(id);
        }

        public async Task CreateCategoryAsync(UpdateCategory newCategory)
        {
            await _repository.CreateCategoryAsync(newCategory);
        }

        public async Task UpdateCategoryAsync(int id, UpdateCategory updatedCategory)
        {
            await _repository.UpdateCateroryAsync(id,updatedCategory);
        }

        public async Task DeleteCategoryAsync(int? id)
        {
            await _repository.DeleteCategoryAsync(id);
        }
    }
}
