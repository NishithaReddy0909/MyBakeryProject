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
        public void CreateProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            _dbContext.Add(product);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Product> GetProducts()
        {
            var products = _dbContext.Products.ToList();
            return products;
        }
    }
}
