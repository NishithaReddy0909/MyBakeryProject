using MyBakeryProject.Data;
using MyBakeryProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBakeryProject.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private ApplicationDbContext _dbContext = null;
        public ProductRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int CreateProduct(Product product)
        {
            if (product != null)
            {
                _dbContext.Products.Add(product);
                return _dbContext.SaveChanges();
            }
            else
            {
                throw new NullReferenceException(nameof(product));
            }
        }

        public void Delete(int? id)
        {
            if (id == null)
            {
                throw new NullReferenceException();
            }
            var product = _dbContext.Products.SingleOrDefault(p => p.Id == id);
            _dbContext.Remove(product);
            
        }

        public int EditProduct(Product product)
        {
            var productFromDb = _dbContext.Products.SingleOrDefault(p => p.Id == product.Id);
            if (productFromDb != null)
            {
                productFromDb.Name = product.Name;
                productFromDb.Description = product.Description;
                productFromDb.Price = product.Price;
                productFromDb.ImageName = product.ImageName;
                return _dbContext.SaveChanges();
            }
            else
            {
                throw new NullReferenceException();
            }
        }

        public IEnumerable<Product> GetProducts()
        {
             return _dbContext.Products.ToList();
            
        }

        public Product ProductGetById(int id)
        {
            return _dbContext.Products.SingleOrDefault(p => p.Id == id);
            
            
        }
    }
}
