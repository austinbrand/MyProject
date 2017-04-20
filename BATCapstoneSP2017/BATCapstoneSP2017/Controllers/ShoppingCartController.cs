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

        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            //string menuItemName = db.Carts.FirstOrDefault(item => item.ProductId == id).Product.Name;
            string menuItemName = db.Carts.FirstOrDefault(item => item.MenuItemID == id).MenuItem.Name;

            int itemCount = cart.RemoveFromCart(id);
            //int itemCount = cart.RemoveFromCart(id);

            var results = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(menuItemName) + " has been removed from your shopping cart",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };

            return Json(results);
        }

        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            ViewData["CartCount"] = cart.GetCount();
            return PartialView("CartSummary");
        }


    }
}