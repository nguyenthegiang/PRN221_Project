using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using PRN221_Project_ShopOnline.DAO;
using PRN221_Project_ShopOnline.Models;

using Microsoft.AspNetCore.Http;

namespace PRN221_Project_ShopOnline.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View("Views/Login.cshtml");
        }

        [HttpPost]
        public IActionResult Login(string username, string password, string remember)
        {
            //Check login account with Database
            UserDAO dao = new UserDAO();
            User user = dao.Login(username, password);
            if (user == null)
            {
                //user not exists -> back to Login
                ViewBag.Message = "Wrong username or password";
                return View("Views/Login.cshtml");
            } else
            {
                //login success: 

                //set User to Session (for other pages to use)
                HttpContext.Session.SetString("username", user.Username);
                HttpContext.Session.SetString("password", user.Password);
                HttpContext.Session.SetInt32("userId", user.UserId);

                //set User to Cookies (for auto Login)
                if (!HttpContext.Request.Cookies.ContainsKey("userId"))
                {
                    HttpContext.Response.Cookies.Append("username", user.Username);
                    HttpContext.Response.Cookies.Append("password", user.Password);
                }

                //to Home Page
                return Redirect("/");
            }
        }
    }
}
