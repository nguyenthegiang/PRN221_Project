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
    }
}
