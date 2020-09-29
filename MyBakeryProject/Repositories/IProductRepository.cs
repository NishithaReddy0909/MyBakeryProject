using MyBakeryProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBakeryProject.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts();
        void CreateProduct(Product product);
    }
}
