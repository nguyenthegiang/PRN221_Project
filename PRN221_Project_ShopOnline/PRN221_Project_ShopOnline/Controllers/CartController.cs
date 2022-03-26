using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using PRN221_Project_ShopOnline.DAO;
using PRN221_Project_ShopOnline.Models;

using Microsoft.AspNetCore.Http;

namespace PRN221_Project_ShopOnline.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        //Add 1 Product to Cart (Amount=1) {From: Home}
        public IActionResult AddToCart(int productId)
        {
            var view = View("Views/Index.cshtml");

            /*----------Add To Cart----------*/
            //Use Session to get user id
            int UserId = 0;
            try
            {
                UserId = (int)HttpContext.Session.GetInt32("userId");
            } catch (Exception)
            {
                //If Exception -> User is not Login (can't add to cart) -> go to Login Page
                return Redirect("/Login/Index");
            }
            

            //Add to DB (amount = 1)
            CartDAO cartDAO = new CartDAO();
            bool notOutOfStock = cartDAO.AddToCart(UserId, productId, 1);

            //Notify Result
            if (notOutOfStock)
            {
                ViewBag.AddToCartMessage = "Product is added to cart";
            } else
            {
                ViewBag.AddToCartMessage = "Sorry, there is not enough Amount in Stock";
            }

            /*----------Back to Home Page----------*/
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
    }
}
