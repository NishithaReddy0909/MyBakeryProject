using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
        public IActionResult ProductGetById(int id)
        {
            var products = _productrepository.ProductGetById(id);
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

            Product prd = new Product
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImageName = product.ImageName.FileName
            };

            int result = _productrepository.CreateProduct(prd);

            if (result > 0)
            {
                this.UploadImageToFolder(product.ImageName);
            }

            return RedirectToAction("Index", "Products");
        }
        [HttpGet]
        public IActionResult EditProduct(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var product = _productrepository.ProductGetById(id.Value);
            if (product == null)
            {
                return NotFound();

            }
            EditProductViewModel editProduct = new EditProductViewModel
            {
                Id = product.Id,
                Name=product.Name,
                Description=product.Description,
                Price=product.Price,
                //ExistingImageName=product.ImageName
                
            };
            return View(editProduct);
           
        }
        [HttpPost]
        public IActionResult EditProduct(EditProductViewModel editProduct)
        {
            Product product = new Product();
            if (editProduct.ImageName == null)
            {
                if (editProduct.ExistingImageName != null)
                {

                    //product.ImageName = editProduct.ExistingImageName;
                }
            }
            else
            {
                product.ImageName = editProduct.ImageName.FileName;
            }

            product.Id = editProduct.Id;
            product.Name = editProduct.Name;
            product.Description = editProduct.Description;
            product.Price = editProduct.Price;

            int result = _productrepository.EditProduct(product);

            if (result > 0 && editProduct.ImageName != null)
            {
                string folderName = this.UploadImageToFolder(editProduct.ImageName);
                //var imgPath = Path.Combine(folderName, editProduct.ImageName);
                //if (System.IO.File.Exists(imgPath))
                //{
                //    System.IO.File.Delete(imgPath);
                //}
            }

            return RedirectToAction("Index", "Products");
        }

        private string UploadImageToFolder(IFormFile ImageFile)
        {
            string folderName = Path.Combine(_webHostEnvironment.WebRootPath, "images");
            string imgFilePath = Path.Combine(folderName, ImageFile.FileName);
            ImageFile.CopyTo(new FileStream(imgFilePath, FileMode.Create));

            return folderName;
        }
    }
    
}

