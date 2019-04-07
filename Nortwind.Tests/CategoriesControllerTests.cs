using System.Collections.Generic;
using System.Linq;
using BLL.CoreEntities.Entities;
using BLL.CoreEntities.Entities.UpdateEntities;
using BLL.Interfaces.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Moq;
using PL.WebAppMVC.Controllers;
using Xunit;

namespace Nortwind.Tests
{
    public class CategoriesControllerTests
    {
        [Fact]
        public void IndexReturnsAViewResultWithAListOfCategories()
        {
            var mockService = new Mock<ICategoryService>();
            var mockCache = new Mock<IMemoryCache>();
            var mockConfig = new Mock<IConfiguration>();
            mockService.Setup(repo => repo.GetAllCategoriesAsync()).ReturnsAsync(GetTestsCategories);
            var controller = new CategoriesController(mockService.Object);

            var result = controller.Index().Result;

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Category>>(viewResult.Model);
            Assert.Equal(GetTestsCategories().Count(), model.Count());
        }

        [Fact]
        public void DetailsReturnsAViewResultWithChosenCategory()
        {
            var mockService = new Mock<ICategoryService>();
            var mockCache = new Mock<IMemoryCache>();
            var mockConfig = new Mock<IConfiguration>();
            mockService.Setup(repo => repo.GetCategoryByIdAsync(1)).ReturnsAsync(GetTestsCategories().First);
            var controller = new CategoriesController(mockService.Object);

            var result = controller.Details(1).Result;

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Category>(viewResult.Model);
            Assert.Equal(GetTestsCategories().First().CategoryId, model.CategoryId);
        }

        [Fact]
        public void CreateReturnsAViewResultWithCategory()
        {
            var mockService = new Mock<ICategoryService>();
            var mockCache = new Mock<IMemoryCache>();
            var mockConfig = new Mock<IConfiguration>();
            var controller = new CategoriesController(mockService.Object);
            controller.ModelState.AddModelError("Name","Required");

            var category = new UpdateCategory();
            var result = controller.Create(category).Result;

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(category,viewResult.Model);
        }
        private IEnumerable<Category> GetTestsCategories()
        {
            var byteArr = new byte[0];
            var categoriesList = new List<Category>()
            {
                new Category()
                {
                    Picture = byteArr,
                    CategoryName = "first",
                    CategoryId = 1,
                    Description = "first"
                },
                new Category()
                {
                    Picture = byteArr,
                    CategoryName = "second",
                    CategoryId = 2,
                    Description = "second"
                }
                ,
                new Category()
                {
                    Picture = byteArr,
                    CategoryName = "third",
                    CategoryId = 3,
                    Description = "third"
                }
            };

            return categoriesList;
        }
    }
}
