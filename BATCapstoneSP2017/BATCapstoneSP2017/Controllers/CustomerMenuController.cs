using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BATCapstoneSP2017.Models;
using System.Data;
using System.Data.Entity;
using System.IO;

namespace BATCapstoneSP2017.Controllers
{
    public class CustomerMenuController : Controller
    {

        WholeContext db = new WholeContext();
        
        // GET: CustomerMenu
        public ActionResult Index()
        {
            //List<MenuItem> MenuItems = new List<MenuItem>();
            //var cart = ShoppingCart.GetCart(this.HttpContext);

            return View(db.MenuItems.ToList());
        }

        //[HttpPost]
        //public ActionResult ShoppingCart()
        //{
            // GET: /Store/AddToCart/5
            // Retrieve the item from the database
            //var addedItem = storeDB.Items.Single(item => item.ID == id);

            //var addedItem = db.MenuItems.Single(item => item.ID == Id);

            // Add it to the shopping cart
            //var cart = ShoppingCart.GetCart(this.HttpContext);

            //int count = cart.AddToCart(addedItem);

            // Display the confirmation message
            //var results = new ShoppingCartRemoveViewModel
            //{
            //    Message = Server.HtmlEncode(addedItem.Name) +
            //        " has been added to your shopping cart.",
            //    CartTotal = cart.GetTotal(),
            //    CartCount = cart.GetCount(),
            //    ItemCount = count,
            //    DeleteId = id
            //};
            //return Json(results);

            // Go back to the main store page for more shopping
           // return RedirectToAction("Index");
        //}
        }
    }


