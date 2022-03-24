using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using PRN221_Project_ShopOnline.DAO;
using PRN221_Project_ShopOnline.Models;

namespace PRN221_Project_ShopOnline.Controllers
{
    public class AccountManagerController : Controller
    {
        public IActionResult Index()
        {
            //Get data from DB
            UserDAO dao = new UserDAO();
            List<User> users = dao.GetAllAccounts().ToList();

            //Set data to View
            ViewBag.Users = users;

            return View("Views/AccountManager.cshtml");
        }
    }
}
