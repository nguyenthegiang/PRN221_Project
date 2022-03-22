using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRN221_Project_ShopOnline.Controllers
{
    public class ProductManagerController : Controller
    {
        public IActionResult Index()
        {
            return View("Views/ProductManager.cshtml");
        }
    }
}
