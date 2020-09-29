using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyBakeryProject.Data.Configuarations;
using MyBakeryProject.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyBakeryProject.Data.Configuarations
{
    public class ProductConfiguaration : IEntityTypeConfiguration<Product>
    {
       public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name).HasColumnName("ProductName");
            builder.Property(p => p.ImageName).HasColumnName("ImageFileName");
        }
    }
}
