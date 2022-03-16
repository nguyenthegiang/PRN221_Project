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
            //if has Cookies -> set username & password for auto login
            ViewBag.CookieUsername = "";
            ViewBag.CookiePassword = "";
            if (HttpContext.Request.Cookies.ContainsKey("username"))
            {
                ViewBag.CookieUsername = HttpContext.Request.Cookies["username"];
                ViewBag.CookiePassword = HttpContext.Request.Cookies["password"];
            }

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
                    CookieOptions userOptions = new CookieOptions();
                    userOptions.Expires = new DateTimeOffset(DateTime.Now.AddHours(1)); //cookie last for 1 hour
                    HttpContext.Response.Cookies.Append("username", user.Username, userOptions);
                    
                    CookieOptions passwordOptions = new CookieOptions();
                    //if user choose [Remember Me] -> save password
                    if (remember != null)
                    {
                        passwordOptions.Expires = new DateTimeOffset(DateTime.Now.AddHours(1));
                    } else
                    {
                        passwordOptions.Expires = new DateTimeOffset(DateTime.Now);
                    }
                    HttpContext.Response.Cookies.Append("password", user.Password);
                }

                //to Home Page
                return Redirect("/");
            }
        }

        public IActionResult Logout()
        {
            //Remove Session
            HttpContext.Session.Remove("userId");
            HttpContext.Session.Remove("username");
            HttpContext.Session.Remove("password");

            //back to home
            return Redirect("/");
        }

        public IActionResult Signup()
        {
            return View("Views/Login.cshtml");
        }
    }
}
