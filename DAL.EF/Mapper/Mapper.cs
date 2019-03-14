using System.Collections.Generic;
using BLL.CoreEntities.Entities;
using DAL.EF.Models;

namespace DAL.EF.Mapper
{
    public static class Mapper
    {
        #region Categories

        public static IEnumerable<Category> ToEnumerableCategoryDto(IEnumerable<Categories> categorieses)
        {
            var categoriesDtoList = new List<Category>();

            foreach (var category in categorieses)
            {
                categoriesDtoList.Add(ToCategoryDto(category));
            }

            return categoriesDtoList;
        }

        public static Category ToCategoryDto(Categories dalCategory) => new Category()
        {
            CategoryId = dalCategory.CategoryId,
            CategoryName = dalCategory.CategoryName,
            Description =  dalCategory.Description
        };

        public static Categories ToCategoriesDal(Category dtoCategory) =>new Categories()
        {
            CategoryName = dtoCategory.CategoryName,
            Description = dtoCategory.Description
        };

        #endregion


    }
}