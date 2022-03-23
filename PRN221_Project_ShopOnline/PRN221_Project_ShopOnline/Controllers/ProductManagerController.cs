using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using PRN221_Project_ShopOnline.Models;
using PRN221_Project_ShopOnline.DAO;
using Microsoft.AspNetCore.Http;

namespace PRN221_Project_ShopOnline.Controllers
{
    public class ProductManagerController : Controller
    {
        public IActionResult Index()
        {
            //Get info of this Seller from Session
            int sellerId = (int) HttpContext.Session.GetInt32("userId");
            string sellerName = HttpContext.Session.GetString("username");

            //Get list products of this Seller for Manage
            ProductDAO productDAO = new ProductDAO();
            List<Product> products = productDAO.GetProductsBySeller(sellerId).ToList();

            //Get list category for [Create Product]
            CategoryDAO categoryDAO = new CategoryDAO();
            List<Category> categories = categoryDAO.GetAllCategories().ToList();

            //Set data to View
            ViewBag.Products = products;
            ViewBag.Categories = categories;
            //User info for Seller in [Create Product]
            ViewBag.SellerId = sellerId;
            ViewBag.SellerName = sellerName;

            return View("Views/ProductManager.cshtml");
        }
    }
}
