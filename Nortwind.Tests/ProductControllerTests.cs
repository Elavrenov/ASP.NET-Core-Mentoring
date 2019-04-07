using System.Collections.Generic;
using System.Linq;
using BLL.CoreEntities.Entities;
using BLL.CoreEntities.Entities.UpdateEntities;
using BLL.Interfaces.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PL.WebAppMVC.Controllers;
using Xunit;

namespace Nortwind.Tests
{
    public class ProductControllerTests
    {
        [Fact]
        public void IndexReturnsAViewResultWithAListOfProducts()
        {
            var mockService = new Mock<IProductService>();
            mockService.Setup(repo => repo.GetAllProductsAsync()).ReturnsAsync(GetTestsCategories);
            var controller = new ProductsController(mockService.Object);

            var result = controller.Index().Result;

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Product>>(viewResult.Model);
            Assert.Equal(GetTestsCategories().Count(), model.Count());
        }

        [Fact]
        public void CreateReturnsAViewResultWithProduct()
        {
            var mockService = new Mock<IProductService>();
            var controller = new ProductsController(mockService.Object);
            controller.ModelState.AddModelError("Name", "Required");

            var product = new UpdateProduct();
            var result = controller.Create(product).Result;

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(product, viewResult.Model);
        }
        private IEnumerable<Product> GetTestsCategories()
        {
            var categoriesList = new List<Product>()
            {
                new Product()
                {
                    ProductId = 1,
                    ProductName = "First"
                },
                new Product()
                {
                    ProductId = 2,
                    ProductName = "Second"
                }
                ,
                new Product()
                {
                    ProductId = 3,
                    ProductName = "Third"
                }
            };

            return categoriesList;
        }
    }
}