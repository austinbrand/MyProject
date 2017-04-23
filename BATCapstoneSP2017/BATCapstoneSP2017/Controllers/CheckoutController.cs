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
        const String PromoCode = "FREE";

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
            //order.AspNetUser = user.Email;
            order.AspNetUsersID = "5f8c1ea6-5717-41ae-894b-47436d8b83da";
            order.Status = "Open";
            

            TryUpdateModel(order);

            try
            {
                if (string.Equals(values["PromoCode"], PromoCode, StringComparison.OrdinalIgnoreCase) == false)
                {
                    return View(order);
                }
                else
                {
                    //order.CustomerUserName = User.Identity.Name;
                    order.Email = User.Identity.Name;
                    order.Date = DateTime.Now;
                    
                    // Save the order
                    db.Orders.Add(order);
                    db.SaveChanges();

                    // Process order
                    var cart = ShoppingCart.GetCart(this.HttpContext);
                    cart.CreateOrder(order);

                    db.SaveChanges(); //we have received the total amount lets update it

                    //return RedirectToAction("Complete", new { id = order.ID });
                    return RedirectToAction("Complete", new {id = order.ID});
                }
            }
            catch
            {
                // I hope this works too lol 
                return RedirectToAction("Complete", new { id = order.ID });
            }
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