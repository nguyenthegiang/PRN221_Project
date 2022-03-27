using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using PRN221_Project_ShopOnline.Models;
using PRN221_Project_ShopOnline.DAO;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.Extensions.Configuration;

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
        public IActionResult AddProduct(string name, string description, int price, 
            IFormFile image, int CategoryID, int SellerID, int amount)
        {
            try
            {
                //Get Image Path from JSON
                var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                string rootdir = config.GetSection("ImageRootPath").Value.ToString();

                //Copy Image file to Project Folder
                string filename = Path.Combine(rootdir, image.FileName);
                using (var fs = new FileStream(filename, FileMode.Create))
                {
                    image.CopyTo(fs);
                }

                //Set data
                Product product = new Product();
                product.ProductName = name;
                product.Description = description;
                product.SellPrice = price;
                product.ImageLink = image.FileName;
                product.CategoryId = CategoryID;
                product.SellerId = SellerID;
                product.Amount = amount;

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

        //Go to Edit Form
        public IActionResult EditProduct(int productId)
        {
            //Get info of this Seller from Session
            int sellerId = (int)HttpContext.Session.GetInt32("userId");
            string sellerName = HttpContext.Session.GetString("username");

            //Get product to edit
            ProductDAO productDAO = new ProductDAO();
            Product product = productDAO.GetProductByID(productId);

            //Get list category for [Edit Product]
            CategoryDAO categoryDAO = new CategoryDAO();
            List<Category> categories = categoryDAO.GetAllCategories().ToList();

            //Set data to View
            ViewBag.Product = product;
            ViewBag.Categories = categories;
            //User info for Seller in [Edit Product]
            ViewBag.SellerId = sellerId;
            ViewBag.SellerName = sellerName;

            return View("Views/EditProduct.cshtml");
        }

        //Update Product in DB
        [HttpPost]
        public IActionResult UpdateProduct(int id, string name, string description, int price,
            IFormFile image, int CategoryID, int SellerID, int amount)
        {
            try
            {
                //if user does not change image -> Keep old image
                if (image == null)
                {
                    //Get old product
                    ProductDAO productDAO = new ProductDAO();
                    Product oldProduct = productDAO.GetProductByID(id);

                    //Set data
                    Product product = new Product();
                    product.ProductId = id;
                    product.ProductName = name;
                    product.Description = description;
                    product.SellPrice = price;
                    product.ImageLink = oldProduct.ImageLink;   //not change image
                    product.CategoryId = CategoryID;
                    product.SellerId = SellerID;
                    product.Amount = amount;

                    //Update to DB
                    ProductDAO dao = new ProductDAO();
                    dao.EditProduct(product);
                }
                else
                {
                    //Get Image Path from JSON
                    var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                    string rootdir = config.GetSection("ImageRootPath").Value.ToString();

                    //Copy Image file to Project Folder
                    string filename = Path.Combine(rootdir, image.FileName);
                    using (var fs = new FileStream(filename, FileMode.Create))
                    {
                        image.CopyTo(fs);
                    }

                    //Set data
                    Product product = new Product();
                    product.ProductId = id;
                    product.ProductName = name;
                    product.Description = description;
                    product.SellPrice = price;
                    product.ImageLink = image.FileName;
                    product.CategoryId = CategoryID;
                    product.SellerId = SellerID;
                    product.Amount = amount;

                    //Update to DB
                    ProductDAO dao = new ProductDAO();
                    dao.EditProduct(product);
                }
            }
            catch (Exception)
            {
                //Wrong input -> notify
                ViewBag.Message = "Wrong input! Please try again";
            }

            //Back to Manage Product
            SetDataToView();

            return View("Views/ProductManager.cshtml");
        }

        //Delete product in DB
        [HttpPost]
        public IActionResult DeleteProduct(int ProductID)
        {
            //Delete in DB
            ProductDAO dao = new ProductDAO();
            bool result = dao.DeleteProductById(ProductID);
            //if Delete fail -> notify
            if (!result)
            {
                ViewBag.Message = "Something went wrong! Please try again";
            }

            //Back to Manage Product
            SetDataToView();

            return View("Views/ProductManager.cshtml");
        }
    }
}
