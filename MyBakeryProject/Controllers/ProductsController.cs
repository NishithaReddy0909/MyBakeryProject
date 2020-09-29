using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MyBakeryProject.Data;
using MyBakeryProject.Models;
using MyBakeryProject.Repositories;
using MyBakeryProject.ViewModels;

namespace MyBakeryProject.Controllers
{
    public class ProductsController : Controller
    {
        private IProductRepository _productrepository = null;
        private IWebHostEnvironment _webHostEnvironment = null;
        public ProductsController(IProductRepository productRepository,IWebHostEnvironment webHostEnvironment)
        {
            _productrepository = productRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var products = _productrepository.GetProducts();
            return View(products);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ProductViewModel product)
        {
           
                if (!ModelState.IsValid)
                {
                    return View();
                }

                string folderName = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                string imgFilePath = Path.Combine(folderName, product.ImageFile.FileName);
                product.ImageFile.CopyTo(new FileStream(imgFilePath, FileMode.Create));

                Product prd = new Product
                {
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    ImageName = product.ImageFile.FileName
                };
                    _productrepository.CreateProduct(prd);

            return RedirectToAction("Index", "Products");
        }
    }
}
