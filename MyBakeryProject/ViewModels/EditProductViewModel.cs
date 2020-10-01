using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBakeryProject.ViewModels
{
    public class EditProductViewModel:ProductViewModel
    {
        public int Id  { get; set; }
        public IFormFile ExistingImageName { get; set; }

        
    }
}
