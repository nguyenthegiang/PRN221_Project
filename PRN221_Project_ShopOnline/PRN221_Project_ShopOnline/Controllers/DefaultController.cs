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

            //set to ViewBag
            ViewBag.Categories = categories;
            ViewBag.Products = products;
            //no selected category
            ViewBag.SelectedCategory = 0;

            return view;
        }

        public IActionResult FindProductByCategory(int cid)
        {
            var view = View("Views/Index.cshtml");

            //Get list Category
            CategoryDAO categoryDao = new CategoryDAO();
            List<Category> categories = categoryDao.GetAllCategories().ToList();

            //Get list Products by Category
            ProductDAO productDao = new ProductDAO();
            List<Product> products = productDao.GetProductsByCategory(cid).ToList();

            //set to ViewBag
            ViewBag.Categories = categories;
            ViewBag.Products = products;
            //Selected Category (for different display)
            ViewBag.SelectedCategory = cid;

            return view;
        }

        public IActionResult SearchProductByName(string ProductName)
        {
            var view = View("Views/Index.cshtml");

            //Get list Category
            CategoryDAO categoryDao = new CategoryDAO();
            List<Category> categories = categoryDao.GetAllCategories().ToList();

            //Get list Products by Name
            ProductDAO productDao = new ProductDAO();
            List<Product> products = productDao.SearchProductByName(ProductName).ToList();

            //set to ViewBag
            ViewBag.Categories = categories;
            ViewBag.Products = products;
            //no selected category
            ViewBag.SelectedCategory = 0;

            return view;
        }
    }
}
