using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.CoreEntities.Entities;
using BLL.CoreEntities.Entities.UpdateEntities;
using BLL.Interfaces.Interfaces;
using DAL.Interfaces.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _cache;
        private const int DefaultCachingTimeInMins = 1;


        public CategoryService(ICategoryRepository repository, IConfiguration configuration, IMemoryCache cache)
        {
            _repository = repository;
            _configuration = configuration;
            _cache = cache;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _repository.GetAllCategoriesAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int? id)
        {
            if (id <= 0)
            {
                throw new ArgumentException($"id cant equals or less  zero");
            }

            return await _repository.GetCategoryById(id);
        }

        public async Task CreateCategoryAsync(UpdateCategory newCategory)
        {
            await _repository.CreateCategoryAsync(newCategory);
        }

        public async Task UpdateCategoryAsync(int id, UpdateCategory updatedCategory)
        {
            if (id <= 0)
            {
                throw new ArgumentException($"id cant equals or less zero");
            }

            await _repository.UpdateCateroryAsync(id, updatedCategory);
        }

        public async Task<byte[]> GetPictureByCategoryId(int? id)
        {
            if (id < 0)
            {
                throw new ArgumentException($"wrong id");
            }

            if (_cache.TryGetValue(id, out var cacheImageChecker))
            {
                return ((byte[])cacheImageChecker);
            }

            var dbImage = await _repository.GetCategoryById(id).ContinueWith(x => x.Result.Picture);

            var cachingTime = DefaultCachingTimeInMins;

            if (int.TryParse(_configuration.GetSection("CachingTime").Value, out var cachingTimeInMin))
            {
                cachingTime = cachingTimeInMin;
            }

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(cachingTime));

            _cache.Set(id, dbImage, cacheEntryOptions);

            return dbImage;
        }
    }
}