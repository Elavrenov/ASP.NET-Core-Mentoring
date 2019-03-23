using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BLL.CoreEntities.Entities;
using BLL.CoreEntities.Entities.UpdateEntities;
using DAL.EF.Models;
using DAL.Interfaces.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Repositories
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
            byte[] pictureBytes = null;

            if (newCategory.Picture != null)
            {
                using (var ms = new MemoryStream())
                {
                    await newCategory.Picture.CopyToAsync(ms);
                    pictureBytes = GetDbValidPictureBytes(ms.ToArray());
                }
            }

            var category = Mapper.Mapper.ToCategoriesDal(newCategory, pictureBytes);

            if (!IsExistedCategory(category).Result)
            {
                await _context.Categories.AddAsync(category);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateCateroryAsync(int id, UpdateCategory updatedCategory)
        {
            var existingCategory = await _context.Categories.SingleOrDefaultAsync(cat => cat.CategoryId == id);

            if (existingCategory == null)
            {
                throw new ArgumentException($"Cant update this item");
            }

            if (!string.Equals(existingCategory.CategoryName, updatedCategory.CategoryName, StringComparison.OrdinalIgnoreCase)
                || !string.Equals(existingCategory.Description, updatedCategory.Description, StringComparison.OrdinalIgnoreCase))
            {
                existingCategory.CategoryName = updatedCategory.CategoryName;
                existingCategory.Description = updatedCategory.Description;
            }

            if (updatedCategory.Picture != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await updatedCategory.Picture.CopyToAsync(memoryStream);

                    var plArray = memoryStream.ToArray();

                    if (!IsEqualsPictures(existingCategory.Picture, plArray))
                    {
                        var byteMaskPicture = GetDbValidPictureBytes(plArray);
          
                        existingCategory.Picture = byteMaskPicture;
                    }
                }
            }

            await _context.SaveChangesAsync();
        }

        private byte[] GetDbValidPictureBytes(byte[] plByteArray)
        {
            byte[] byteMaskPicture = new byte[78];
            Array.Resize(ref byteMaskPicture, 78 + plByteArray.Length);
            for (int i = 78, j = 0; i < plByteArray.Length; i++, j++)
            {
                byteMaskPicture[i] = plByteArray[j];
            }

            return byteMaskPicture;
        }
        private bool IsEqualsPictures(byte[] dBytes, byte[] plBytes)
        {
            if (dBytes.Skip(78).Count() != plBytes.Length)
            {
                return false;
            }

            return true;
        }
        private async Task<bool> IsExistedCategory(Categories category)
        {
            var categoryChecker = await _context.Categories.FirstOrDefaultAsync(x =>
                string.Equals(x.CategoryName, category.CategoryName, StringComparison.OrdinalIgnoreCase));

            if (categoryChecker == null)
            {
                return false;
            }

            throw new ArgumentException($"This item is already exist");
        }
    }
}