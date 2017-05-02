using BATCapstoneSP2017.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BATCapstoneSP2017.Controllers
{
    public class CheckoutController : Controller
    {
        WholeContext db = new WholeContext();
        //const String PromoCode = "FREE";

        public ActionResult AddressAndPayment()
        {
            //var previousOrder = storeDB.Orders.FirstOrDefault(x => x.Username == User.Identity.Name);

            //var currentOrder = db.Orders.FirstOrDefault(m => m.Email == User.Identity.Name);

            /* (currentOrder != null)
            {
                return View(currentOrder);
            }*/

            return View();
            
        }

        [HttpPost]
        public ActionResult AddressAndPayment(FormCollection values)
        {
            var order = new Order();
            var user = new AspNetUser();
            //var shoppingCartTotal = new ShoppingCart();
            OrderMenuItem orderTotal = new OrderMenuItem();
            //order.AspNetUser = user.Email; 
            order.Status = "Open";

            var cart = ShoppingCart.GetCart(this.HttpContext);
            order.Total = cart.GetTotal();


            TryUpdateModel(order);

            //order.CustomerUserName = User.Identity.Name;
            order.Email = User.Identity.Name;
            order.Date = DateTime.Now;
            //order.Total = orderTotal.Total;
            

            // Save the order
            db.Orders.Add(order);
            db.SaveChanges();

            // Process order
            //var cart = ShoppingCart.GetCart(this.HttpContext);
            cart.CreateOrder(order);

            //db.SaveChanges(); //we have received the total amount lets update it

            //return RedirectToAction("Complete", new { id = order.ID });
            return RedirectToAction("Complete", new { id = order.ID });


            //original code below this comment starting at the try
            //try
            //{
            //    if (string.Equals(values["PromoCode"], PromoCode, StringComparison.OrdinalIgnoreCase) == false)
            //    {
            //        //order.CustomerUserName = User.Identity.Name;
            //        order.Email = User.Identity.Name;
            //        order.Date = DateTime.Now;

            //        // Save the order
            //        db.Orders.Add(order);
            //        db.SaveChanges();

            //        // Process order
            //        var cart = ShoppingCart.GetCart(this.HttpContext);
            //        cart.CreateOrder(order);

            //        db.SaveChanges(); //we have received the total amount lets update it

            //        //return RedirectToAction("Complete", new { id = order.ID });
            //        return RedirectToAction("Complete", new { id = order.ID });
            //    }
            //    else
            //    {
            //        //order.CustomerUserName = User.Identity.Name;
            //        order.Email = User.Identity.Name;
            //        order.Date = DateTime.Now;
                    
            //        // Save the order
            //        db.Orders.Add(order);
            //        db.SaveChanges();

            //        // Process order
            //        var cart = ShoppingCart.GetCart(this.HttpContext);
            //        cart.CreateOrder(order);

            //        db.SaveChanges(); //we have received the total amount lets update it

            //        //return RedirectToAction("Complete", new { id = order.ID });
            //        return RedirectToAction("Complete", new {id = order.ID});
            //    }
            //}
            //catch
            //{
            //    // redirect it through here 
            //    return RedirectToAction("Complete", new { id = order.ID });
            //}
            //catch 
            //{
            //    // Invalid 
            //    return View(order);
            //}
        }

        // public ActionResult Complete(int id)
        public ActionResult Complete(int id)
        {

            Order orderId = db.Orders.Find(id);

            // Validates the order belongs to the logged in user 
            /*bool isValid = db.Orders.Any(
                o => o.ID == id &&
                     o.Email == User.Identity.Name);*/

            bool isValid = db.Orders.Any(o => o.ID == id && o.Email == User.Identity.Name);


            if (isValid)
            {
                return View(orderId);
            }
            else
            {
                return View("Error");
            }
        }


        
        
    }
}