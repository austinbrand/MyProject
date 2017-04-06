using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BATCapstoneSP2017.Controllers
{
    public class OrderMenuItemsController : Controller
    {
        // GET: OrderMenuItems
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult OrderNow()
        {
            return View();
        }


    }
}