using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using MyBakeryProject;
using MyBakeryProject.Repositories;
using Moq;
using MyBakeryProject.Models;
using MyBakeryProject.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using MyBakeryProject.ViewModels;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace MyBakeryProject.UnitTest
{
     public class ProductsControllerTests
    {
        private Mock<IProductRepository> _mockrepository = null;
        private Mock<IWebHostEnvironment> _mockwebhost = null;
        public ProductsControllerTests()
       
        {
            _mockrepository = new Mock<IProductRepository>();
            _mockwebhost = new Mock<IWebHostEnvironment>();
        }
        private List<Product> GetProducts()
        {
            return new List<Product>
          {
             new Product { Id=1,Name="bakery-pastries",Description="Delicious Baked Pastry",Price=50.00f,ImageName="bakery-pastries.jpg"},
              new Product { Id=2,Name="CupCake",Description="Yummy Cakes",Price=30.5f,ImageName="CupCake.jpg"}
          };

        }
        [Fact]
        public void Index_GetProductsList_ReturnsView()
        {
            //Arrange
            _mockrepository.Setup(p => p.GetProducts()).Returns(GetProducts);
           
            var Controller = new ProductsController(_mockrepository.Object,_mockwebhost.Object);

            //Act
            var actual = Controller.Index();

            //Assert
            //var ViewResult = Assert.IsType<viewResult>(actual);
            var ViewResult = Assert.IsType<ViewResult>(actual);
            var model = Assert.IsAssignableFrom<List<Product>>(ViewResult.ViewData.Model);
            Assert.Equal(2, model.Count);


        }
        [Fact]
        public void Create_GivenInvalidProduct_ReturnsBadRequest()
        {
            var Controller = new ProductsController(_mockrepository.Object, _mockwebhost.Object);
            var actual = Controller.Create(null);
            Assert.IsType<BadRequestObjectResult>(actual);
        }
        public ProductViewModel AddProduct()
        {
            var _mockFormFile = new Mock<IFormFile>();
            var PhysicalImagePath = new FileInfo(@"C:\DXC\ASP.Net Core\MyBakeryProject\MyBakeryProject\wwwroot\images\bakery-pastries.jpg");
            var ImagePath = PhysicalImagePath.Name;
            _mockFormFile.Setup(i => i.FileName).Returns(ImagePath);
            return new ProductViewModel
            {
                Name = "bakery-pastries",
                Description = "Delicious Baked Pastry",
                Price = 50.00f,
                ImageName = _mockFormFile.Object,
            };
        }
        [Fact]
        public void Create_AddProductRedirectToIndexPage_WhenModelStateisValid()
        {
            var Controller = new ProductsController(_mockrepository.Object, _mockwebhost.Object);
            _mockrepository.Setup(a => a.CreateProduct(It.IsAny<Product>())).Verifiable();
            var add = AddProduct();
            var actual = Controller.Create(add);
            var redirectResult = Assert.IsType<RedirectToActionResult>(actual);
            Assert.NotNull(redirectResult.ControllerName);
            Assert.Equal("Index", redirectResult.ActionName);
        }

    }
}

