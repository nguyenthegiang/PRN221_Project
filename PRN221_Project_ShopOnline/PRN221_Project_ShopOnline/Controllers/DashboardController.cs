using Microsoft.AspNetCore.Mvc;

using PRN221_Project_ShopOnline.DAO;
using PRN221_Project_ShopOnline.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace PRN221_Project_ShopOnline.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            SetDataToView();

            var view = View("Views/SaleDashboard.cshtml");
            return view;
        }

        private void SetDataToView()
        {
            //num account, num product, num order, top 3 most sell, top 3 least sell, recent order, count prod by cat
            //num account
            UserDAO userDAO = new UserDAO();
            double numAcc = userDAO.GetAllAccounts().ToList().Count;
            ViewBag.NumAcc = numAcc;

            //num product
            ProductDAO productDAO = new ProductDAO();
            double numProd = productDAO.GetAllProducts().ToList().Count;
            ViewBag.NumProd = numProd;

            //num order
            ElectronicShopPRN221Context context = new ElectronicShopPRN221Context();
            double numOrd = context.Orders.ToList().Count;
            ViewBag.NumOrd = numOrd;

            var q = (from h in context.OrderDetails
                     group h by new { h.ProductId } into hh
                     select new OrderDetail
                     {
                         ProductId = hh.Key.ProductId,
                         Quantity = hh.Sum(s => s.Quantity)
                     }).OrderByDescending(i => i.Quantity).ToList<OrderDetail>();
            List<int> a = new List<int>();
            for (int i = 0; i < 3; i++)
            {
                a.Add(q[i].ProductId);
            }
            List<Product> mostSellProducts = (List<Product>)context.Products.Where(p => a.Contains(p.ProductId)).ToList();
            ViewBag.MostSellProd = mostSellProducts;

            // count prod by cate
            var prodCat = (from h in context.Products
                           group h by new { h.CategoryId } into hh
                           select new Product
                           {
                               CategoryId = hh.Key.CategoryId,
                               Amount = hh.Count()
                           }).ToList<Product>();
            
            foreach (Product prod in prodCat)
            {
                prod.ProductName = context.Categories.FirstOrDefault(c => c.CategoryId == prod.CategoryId).CategoryName;
                
            }
           
            ViewBag.ProdPerCate = prodCat;

            //total revenue
            context = new ElectronicShopPRN221Context();
            List<OrderDetail> allOrderDetail = context.OrderDetails.ToList();
            int totalRevenue = 0;
            foreach (OrderDetail o in allOrderDetail)
            {
                totalRevenue += o.ProductPrice;
            }
            ViewBag.TotalRevenue = totalRevenue;

        }
    }
}
