using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BLL.CoreEntities.Entities;
using BLL.CoreEntities.Entities.UpdateEntities;
using DAL.EF.Models;
using DAL.Interfaces.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Mapper
{
    public static class Mapper
    {
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

        public static Categories ToCategoriesDal(UpdateCategory dtoCategory, byte[] pictureBytes)
        {
            return new Categories
            {
                CategoryName = dtoCategory.CategoryName,
                Description = dtoCategory.Description,
                Picture = pictureBytes
            };
        }

        public static UpdateCategory ToUpdateCategoryModel(Category category)
        {
            FormFile pictureFile = null;

            if (category.Picture != null)
            {
                var stream = new MemoryStream(category.Picture);

                var pictureFormFile = new FormFile(stream, 0, stream.Length, null, category.CategoryName)
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "image/jpeg"
                };

                pictureFile = pictureFormFile;
            }

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

        public static UpdateProduct ToUpdatedProduct(Products product)
        {
            return new UpdateProduct
            {
                CategoryIdNames = product.Category.CategoryName,
                ProductName = product.ProductName,
                UnitsInStock = product.UnitsInStock,
                QuantityPerUnit = product.QuantityPerUnit,
                UnitsOnOrder = product.UnitsOnOrder,
                Discontinued = product.Discontinued,
                ReorderLevel = product.ReorderLevel,
                UnitPrice = product.UnitPrice,
                SupplierIdNames = product.Supplier.CompanyName
            };
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

        public static Products ToProductsDal(UpdateProduct dtoProduct, NorthwindContext context)
        {
            return new Products
            {
                Category = context.Categories.First(x =>
                    String.Equals($"{x.CategoryName}", $"{dtoProduct.CategoryIdNames}", StringComparison.OrdinalIgnoreCase)),
                ProductName = dtoProduct.ProductName,
                UnitsInStock = dtoProduct.UnitsInStock,
                Supplier = context.Suppliers.First(x =>
                    String.Equals($"{x.CompanyName}", $"{dtoProduct.SupplierIdNames}", StringComparison.OrdinalIgnoreCase)),
                QuantityPerUnit = dtoProduct.QuantityPerUnit,
                UnitsOnOrder = dtoProduct.UnitsOnOrder,
                Discontinued = dtoProduct.Discontinued,
                ReorderLevel = dtoProduct.ReorderLevel
            };
        }

        #endregion
    }
}