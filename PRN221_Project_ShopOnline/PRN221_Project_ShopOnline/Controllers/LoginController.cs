﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRN221_Project_ShopOnline.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View("Views/Login.cshtml");
        }
    }
}