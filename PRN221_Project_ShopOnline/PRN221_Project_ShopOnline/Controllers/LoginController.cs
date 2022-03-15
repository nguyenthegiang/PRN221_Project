﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using PRN221_Project_ShopOnline.DAO;
using PRN221_Project_ShopOnline.Models;

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
                //login success -> to Home Page
                return Redirect("/");
            }
        }
    }
}
