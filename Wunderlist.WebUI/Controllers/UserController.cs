﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wunderlist.WebUI.Models;

namespace Wunderlist.WebUI.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        [HttpGet]
        public ActionResult Singup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Singup(User user)
        {
            if (ModelState.IsValid)
            {

                return RedirectToAction("Index", "Home");
            }
            return new EmptyResult();
        }
    }
}