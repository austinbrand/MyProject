﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BATCapstoneSP2017.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            //ViewBag.Message = "About Us At The Crystal Cafe";

            return View();
        }

        public ActionResult Contact()
        {
            //ViewBag.Message = "Contact Us";

            return View();
        }
    }
}