using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace MyBakeryProject.ViewModels
{
    public class ProductViewModel
    {
        [Required]
        [Display(Name="Prodcut Name")]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        [Required]
        public float Price { get; set; }

        [Required]
        [Display(Name="Image FileName")]
        public IFormFile ImageName { get; set; }
    }
}
