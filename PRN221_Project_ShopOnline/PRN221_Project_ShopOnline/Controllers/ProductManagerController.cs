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
            SetDataToView();

            return View("Views/ProductManager.cshtml");
        }

        //Make this a different method because there is reuse
        private void SetDataToView()
        {
            //Get info of this Seller from Session
            int sellerId = (int)HttpContext.Session.GetInt32("userId");
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
        }

        [HttpPost]
        public IActionResult AddProduct(string name, string description, string price, 
            string imageLink, string CategoryID, string SellerID, string amount)
        {
            try
            {
                //Convert data
                Product product = new Product();
                product.ProductName = name;
                product.Description = description;
                product.SellPrice = int.Parse(price);
                product.ImageLink = imageLink;
                product.CategoryId = int.Parse(CategoryID);
                product.SellerId = int.Parse(SellerID);
                product.Amount = int.Parse(amount);

                //Add to DB
                ProductDAO dao = new ProductDAO();
                bool addResult = dao.AddProduct(product);
                //if Add fail -> throw exception to notify
                if (!addResult)
                {
                    throw new Exception();
                }
            } catch (Exception)
            {
                //Wrong input -> notify
                ViewBag.Message = "Wrong input! Please try again";
            }

            //Back to Manage Product
            SetDataToView();

            return View("Views/ProductManager.cshtml");
        }

        public IActionResult EditProduct(string productId)
        {
            return View("Views/EditProduct.cshtml");
        }
    }
}
