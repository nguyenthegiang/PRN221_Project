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
            SetDataToView();

            return View("Views/AccountManager.cshtml");
        }

        public void SetDataToView()
        {
            //Get data from DB
            UserDAO dao = new UserDAO();
            List<User> users = dao.GetAllAccounts().ToList();

            //Set data to View
            ViewBag.Users = users;
        }

        public IActionResult DeleteAccount(int UserId)
        {
            //Back to ManageAccount
            SetDataToView();
            return View("Views/AccountManager.cshtml");
        }
    }
}
