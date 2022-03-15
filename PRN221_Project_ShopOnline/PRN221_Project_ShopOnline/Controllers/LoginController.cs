using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Login(string user, string pass, string remember)
        {
            //Check login account with Database
            
            return Redirect("/");
        }
    }
}
