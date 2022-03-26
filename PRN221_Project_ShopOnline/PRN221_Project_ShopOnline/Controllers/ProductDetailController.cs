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
    public class ProductDetailController : Controller
    {
        public IActionResult Index(int ProductID)
        {
            var view = View("Views/ProductDetail.cshtml");

            //Get product Detail
            ProductDAO dao = new ProductDAO();
            Product p = dao.GetProductByID(ProductID);

            //set to ViewBag
            ViewBag.Product = p;

            return view;
        }

        //Add an Amount of Product to DB
        [HttpPost]
        public IActionResult AddManyToCart(int ProductId, int Amount)
        {
            /*---Add to Cart---*/
            //Use Session to get user id
            int UserId = 0;
            try
            {
                UserId = (int)HttpContext.Session.GetInt32("userId");
            }
            catch (Exception)
            {
                //If Exception -> User is not Login (can't add to cart) -> go to Login Page
                return Redirect("/Login/Index");
            }

            //Add to DB
            CartDAO cartDAO = new CartDAO();
            bool notOutOfStock = cartDAO.AddToCart(UserId, ProductId, Amount);

            //Notify Result
            if (notOutOfStock)
            {
                ViewBag.AddToCartMessage = "Product is added to cart";
            }
            else
            {
                ViewBag.AddToCartMessage = "Sorry, there is not enough Amount in Stock";
            }

            /*---Back to Product Detail---*/
            var view = View("Views/ProductDetail.cshtml");

            //Get product Detail
            ProductDAO dao = new ProductDAO();
            Product p = dao.GetProductByID(ProductId);

            //set to ViewBag
            ViewBag.Product = p;

            return view;
        }
    }
}
