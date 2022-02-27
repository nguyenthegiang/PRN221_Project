using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using PRN221_Project_ShopOnline.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using PRN221_Project_ShopOnline.DAO;

namespace PRN221_Project_ShopOnline.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            var view = View("Views/Index.cshtml");

            //Get list Category
            CategoryDAO categoryDao = new CategoryDAO();
            List<Category> categories = categoryDao.GetAllCategories().ToList();

            //Get list Products
            ProductDAO productDao = new ProductDAO();
            List<Product> products = productDao.GetAllProducts().ToList();

            ViewBag.Categories = categories;
            ViewBag.Products = products;

            return view;
        }
    }
}
