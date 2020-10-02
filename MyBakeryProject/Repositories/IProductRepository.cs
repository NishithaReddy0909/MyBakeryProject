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
        Product ProductGetById(int id);
        int CreateProduct(Product product);
        int EditProduct(Product product);
        void Delete(int? id);
    }
}
