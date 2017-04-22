using BATCapstoneSP2017.Models;
using BATCapstoneSP2017.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BATCapstoneSP2017.Controllers
{
    public class ShoppingCartController : Controller
    {

        WholeContext db = new WholeContext();

        // GET: ShoppingCart
        public ActionResult Index()
        {
            
            var cart = ShoppingCart.GetCart(this.HttpContext);

            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };

            return View(viewModel);
        }

        public ActionResult AddToCart(int id)
        {
            //var addedProduct = db.Products.Single(product => product.Id == id);

            var addedMenuItem = db.MenuItems.Single(menuItem => menuItem.ID == id);

            var cart = ShoppingCart.GetCart(this.HttpContext);

            cart.AddToCart(addedMenuItem);

            return RedirectToAction("Index");
        }

        
        public ActionResult RemoveFromCart(int id)
        {
            Cart cartItemToDelete = db.Carts.Find(id);
            
            
            if (cartItemToDelete == null)
            {
                return HttpNotFound();
            }

            return View(cartItemToDelete);

        }


        [HttpPost, ActionName("RemoveFromCart")]
        [ValidateAntiForgeryToken]
        public ActionResult RemovedFromCart(int id)
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            Cart cartItemToDelete = db.Carts.Find(id);
            //Order orderItemToDelete = db.Orders.Find(id);
            
            //string menuItemName = db.Carts.FirstOrDefault(item => item.MenuItemID == id).MenuItem.Name; 

            //int itemCount = cart.RemoveFromCart(id);
            //if (cartItemToDelete.ID == orderItemToDelete.CartId)
            //{
            //    db.Carts.Remove(cartItemToDelete);
            //    db.Orders.Remove(cartItemToDelete);
            //}

            db.Carts.Remove(cartItemToDelete);
            db.SaveChanges();
            return RedirectToAction("Index");
            

            //var results = new ShoppingCartRemoveViewModel
            //{
            //    Message = Server.HtmlEncode(menuItemName) + " has been removed from your shopping cart",
            //    CartTotal = cart.GetTotal(),
            //    CartCount = cart.GetCount(),
            //    ItemCount = itemCount,
            //    DeleteId = id
            //};

            //return Json(results);
        }

        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            ViewData["CartCount"] = cart.GetCount();
            return PartialView("CartSummary");
        }


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}