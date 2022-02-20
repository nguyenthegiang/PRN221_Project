using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PRN221_Project_ShopOnline.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PRN221_Project_ShopOnline.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index(int id)
        {
            var view = View("Views/Index.cshtml");

            ElectronicShopPRN221Context context = new ElectronicShopPRN221Context();
            List<Category> categories = context.Categories.ToList<Category>();

            ViewBag.Categories = new MultiSelectList(categories);

            return view;
        }
    }
}
