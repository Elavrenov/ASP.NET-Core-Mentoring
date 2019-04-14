using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BLL.CoreEntities.Entities;
using BLL.CoreEntities.Entities.UpdateEntities;
using DAL.EF.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

namespace DAL.EF.Mapper
{
    public static class Mapper
    {
        private const int DefaultByteMaskNumber = 78;

        #region Categories

        public static IEnumerable<Category> ToEnumerableCategoryDto(IEnumerable<Categories> categorieses)
        {
            var categoriesDtoList = new List<Category>();

            foreach (var category in categorieses) categoriesDtoList.Add(ToCategoryDto(category));

            return categoriesDtoList;
        }

        public static Category ToCategoryDto(Categories dalCategory)
        {
            byte[] picture = null;
            if (dalCategory.Picture != null)
            {
                picture = dalCategory.Picture.Skip(78).ToArray();
            }
            return new Category
            {
                CategoryId = dalCategory.CategoryId,
                CategoryName = dalCategory.CategoryName,
                Description = dalCategory.Description,
                Picture = picture
            };
        }

        public static Category ToCategory(UpdateCategory dtoCategory)
        {
            byte[] pictureBytes = null;

            if (dtoCategory.Picture != null)
            {
                using (var ms = new MemoryStream())
                {
                    dtoCategory.Picture.CopyTo(ms);
                    pictureBytes = GetDbValidPictureBytes(ms.ToArray());
                }
            }

            return new Category
            {
                CategoryName = dtoCategory.CategoryName,
                Description = dtoCategory.Description,
                Picture = pictureBytes
            };
        }

        public static Categories ToCategoriesDal(Category category) => new Categories()
        {
            CategoryId = category.CategoryId,
            Picture = category.Picture,
            CategoryName = category.CategoryName,
            Description = category.Description
        };

        public static UpdateCategory ToUpdateCategoryModel(Category category)
        {
            if (category.Picture == null)
                return new UpdateCategory()
                {
                    CategoryName = category.CategoryName,
                    Description = category.Description,
                    Picture = null
                };
            var stream = new MemoryStream(category.Picture);

            var pictureFormFile = new FormFile(stream, 0, stream.Length, null, category.CategoryName)
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/jpeg"
            };

            var pictureFile = pictureFormFile;

            return new UpdateCategory()
            {
                CategoryName = category.CategoryName,
                Description = category.Description,
                Picture = pictureFile
            };
        }
        #endregion

        #region Products

        public static IEnumerable<Product> ToEnumerableProductDto(IEnumerable<Products> products)
        {
            var prodctDtoList = new List<Product>();

            foreach (var product in products) prodctDtoList.Add(ToProductDto(product));

            return prodctDtoList;
        }
        public static UpdateProduct ToUpdateProductModel(Product product)
        {
            return new UpdateProduct
            {
                CategoryIdNames = product.Category,
                Discontinued = product.Discontinued,
                ProductName = product.ProductName,
                QuantityPerUnit = product.QuantityPerUnit,
                ReorderLevel = product.ReorderLevel,
                SupplierIdNames = product.Supplier,
                UnitPrice = product.UnitPrice,
                UnitsInStock = product.UnitsInStock,
                UnitsOnOrder = product.UnitsOnOrder
            };
        }

        public static Product ToProductDto(Products dalProducts)
        {
            return new Product
            {
                Category = dalProducts.Category.CategoryName,
                Discontinued = dalProducts.Discontinued,
                ProductId = dalProducts.ProductId,
                ProductName = dalProducts.ProductName,
                QuantityPerUnit = dalProducts.QuantityPerUnit,
                ReorderLevel = dalProducts.ReorderLevel,
                Supplier = dalProducts.Supplier.CompanyName,
                UnitPrice = dalProducts.UnitPrice,
                UnitsInStock = dalProducts.UnitsInStock,
                UnitsOnOrder = dalProducts.UnitsOnOrder
            };
        }

        public static Products ToProductsDal(Product dtoProduct, NorthwindContext context)
        {
            return new Products
            {
                Category = context.Categories.First(x =>
                    String.Equals($"{x.CategoryName}", $"{dtoProduct.Category}", StringComparison.OrdinalIgnoreCase)),
                ProductName = dtoProduct.ProductName,
                UnitsInStock = dtoProduct.UnitsInStock,
                Supplier = context.Suppliers.First(x =>
                    String.Equals($"{x.CompanyName}", $"{dtoProduct.Supplier}", StringComparison.OrdinalIgnoreCase)),
                QuantityPerUnit = dtoProduct.QuantityPerUnit,
                UnitsOnOrder = dtoProduct.UnitsOnOrder,
                Discontinued = dtoProduct.Discontinued,
                ReorderLevel = dtoProduct.ReorderLevel
            };
        }

        public static Product ToProduct(UpdateProduct product) => new Product()
        {
            Category = product.CategoryIdNames,
            Supplier = product.SupplierIdNames,
            QuantityPerUnit = product.QuantityPerUnit,
            ProductName = product.ProductName,
            UnitsOnOrder = product.UnitsOnOrder,
            Discontinued = product.Discontinued,
            UnitsInStock = product.UnitsInStock,
            UnitPrice = product.UnitPrice,
            ReorderLevel = product.ReorderLevel,
        };
        #endregion

        #region private

        private static byte[] GetDbValidPictureBytes(byte[] plByteArray)
        {
            byte[] byteMaskPicture = new byte[DefaultByteMaskNumber];
            Array.Resize(ref byteMaskPicture, DefaultByteMaskNumber + plByteArray.Length);
            for (int i = DefaultByteMaskNumber, j = 0; i < plByteArray.Length; i++, j++)
            {
                byteMaskPicture[i] = plByteArray[j];
            }

            return byteMaskPicture;
        }

        #endregion
    }
}