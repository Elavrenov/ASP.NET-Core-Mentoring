using System.Collections.Generic;
using BLL.CoreEntities.Entities;
using BLL.CoreEntities.Entities.UpdateEntities;
using DAL.EF.Models;

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
            return new Category
            {
                CategoryId = dalCategory.CategoryId,
                CategoryName = dalCategory.CategoryName,
                Description = dalCategory.Description
            };
        }

        public static Categories ToCategoriesDal(UpdateCategory dtoCategory)
        {
            return new Categories
            {
                CategoryName = dtoCategory.CategoryName,
                Description = dtoCategory.Description
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

        public static Products ToProductsDal(UpdateProduct dtoProduct)
        {
            return new Products
            {
                Category = new Categories
                {
                    CategoryName = dtoProduct.CategoryIdNames
                },
                ProductName = dtoProduct.ProductName,
                UnitsInStock = dtoProduct.UnitsInStock,
                Supplier = new Suppliers
                {
                    CompanyName = dtoProduct.SupplierIdNames
                },
                QuantityPerUnit = dtoProduct.QuantityPerUnit,
                UnitsOnOrder = dtoProduct.UnitsOnOrder,
                Discontinued = dtoProduct.Discontinued,
                ReorderLevel = dtoProduct.ReorderLevel
            };
        }

        #endregion
    }
}