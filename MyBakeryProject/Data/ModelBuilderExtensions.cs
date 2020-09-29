using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyBakeryProject.Models;

namespace MyBakeryProject.Data
{
    public static class ModelBuilderExtensions
    {
        public static ModelBuilder SeedData(this ModelBuilder builder)
        {
            builder.Entity<Product>().HasData(
                new Product { Id=1,Name="bakery-pastries",Description="Delicious Baked Pastry",Price=50.00f,ImageName="bakery-pastries.jpg"},
                new Product { Id=2,Name="CupCake",Description="Yummy Cakes",Price=30.5f,ImageName="CupCake.jpg"}

                );
            return builder;
        }
    }
}
