using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace MyBakeryProject.Controllers
{
    
    public class ErrorController : Controller
    {
        [Route("Error/{statuscode}")]
        public IActionResult HttpStatusCode(int StatusCode)
        {
            switch (StatusCode)
            {
             case 404:
                    ViewBag.Statuscode = "404:PageNotFound";
            ViewData["Error Message"] = "Sorry, Resource which you are trying could not found,Please try later";
            break;
                case 400:
                    ViewBag.Statuscode = "400:Bad Request";
                    ViewData["Error Message"] = "Sorry the Resource which you are requested could not be understand by Internal Server";
                    break;
                case 500:
                    ViewBag.Statuscode = "500:Internal Server error";
                    ViewData["Error Message"] = "Sorry, Internal Error, Please try after Sometime or Refresh the Page";
                    break;
            }
            return View();
        }
    }
}
