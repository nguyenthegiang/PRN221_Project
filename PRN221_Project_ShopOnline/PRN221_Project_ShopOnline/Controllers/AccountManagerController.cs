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

        [HttpPost]
        public IActionResult DeleteAccount(int UserId)
        {
            //Delete
            UserDAO dao = new UserDAO();
            bool result = dao.DeleteAccountById(UserId);
            //if delete fail -> notify
            if (!result)
            {
                ViewBag.Message = "Something went wrong! Please try again";
            }

            //Back to ManageAccount
            SetDataToView();
            return View("Views/AccountManager.cshtml");
        }

        //Go to Edit Form
        public IActionResult EditAccount(int UserID)
        {
            //Get account to edit
            UserDAO dao = new UserDAO();
            User user = dao.GetAccountById(UserID);

            //Set data to view
            ViewBag.User = user;

            return View("Views/EditAccount.cshtml");
        }

        //Update Account to DB
        [HttpPost]
        public IActionResult UpdateAccount(int id, string username, string email, string password, int IsSeller, int IsAdmin)
        {
            try
            {
                //Set data
                User user = new User();
                user.UserId = id;
                user.Username = username;
                user.Email = email;
                user.Password = password;
                user.IsSeller = IsSeller;
                user.IsAdmin = IsAdmin;

                //Update to DB
                UserDAO dao = new UserDAO();
                dao.EditAccount(user);
            }
            catch (Exception)
            {
                //Wrong input -> notify
                ViewBag.Message = "Something went wrong! Please try again";
            }

            //Back to ManageAccount
            SetDataToView();
            return View("Views/AccountManager.cshtml");
        }
    }
}
