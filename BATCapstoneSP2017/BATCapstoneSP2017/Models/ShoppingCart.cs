using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BATCapstoneSP2017.Models
{
    public class ShoppingCart
    {
        WholeContext db = new WholeContext();

        public string ShoppingCartID { get; set; }

        public const string CartSessionKey = "cartID";

        public static ShoppingCart GetCart(HttpContextBase context)
        {
            var cart = new ShoppingCart();

            // Add GetCartID method
            cart.ShoppingCartID = cart.GetCartID(context);

            return cart;
        }

        public static ShoppingCart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }

        public void AddToCart(MenuItem menuItem)
        {

            var cartItem = db.Carts.SingleOrDefault(c => c.CartID == ShoppingCartID && c.MenuItemID == menuItem.ID);

            if (cartItem == null)
            {
                cartItem = new Cart
                {
                    MenuItemID = menuItem.ID,
                    CartID = ShoppingCartID,
                    Count = 1,
                    DateCreated = DateTime.Now
                };
                db.Carts.Add(cartItem);
                //db.Carts.Add(cartItem);
            }
            else
            {
                cartItem.Count++;
            }

            db.SaveChanges();
        }

        public int RemoveFromCart(int id)
        {
            
            var cartItem = db.Carts.SingleOrDefault(cart => cart.CartID == ShoppingCartID && cart.MenuItemID == id);

            int itemCount = 0;

            if (cartItem != null)
            {
                if (cartItem.Count > 1)
                {
                    cartItem.Count--;
                    itemCount = cartItem.Count;
                }
                else
                {
                    db.Carts.Remove(cartItem);
                }

                db.SaveChanges();
            }
            return itemCount;
        }

        public void EmptyCart()
        {
            var cartItems = db.Carts.Where(cart => cart.CartID == ShoppingCartID);

            foreach (var cartItem in cartItems)
            {
                db.Carts.Remove(cartItem);
            }
            db.SaveChanges();
        }

        public List<Cart> GetCartItems()
        {
            return db.Carts.Where(cart => cart.CartID == ShoppingCartID).ToList();
        }

        public int GetCount()
        {
            int? count = (from cartItems in db.Carts where cartItems.CartID == ShoppingCartID select (int?) cartItems.Count).Sum();

            return count ?? 0;
        }

        public decimal GetTotal()
        {
            decimal? total = (from cartItems in db.Carts
                where cartItems.CartID == ShoppingCartID
                select (int?) cartItems.Count * cartItems.MenuItem.Price).Sum();

            return total ?? decimal.Zero;
        }

        public int CreateOrder(Order customerOrder)
        {
            decimal orderTotal = 0;

            var cartItems = GetCartItems();

            foreach (var item in cartItems)
            {
                var orderedMenuItems = new OrderMenuItem
                {
                    MenuItemID = item.MenuItemID,
                    OrderID = customerOrder.ID,
                    Quantity = item.Count

                };

                  orderTotal += (item.Count * item.MenuItem.Price); 
                //orderTotal += (item.Count * item.MenuItem.Price);

                db.OrderMenuItems.Add(orderedMenuItems);
            }

            customerOrder.Total = orderTotal;
            

            db.SaveChanges();

            EmptyCart();

            return customerOrder.ID;
        }

        public string GetCartID(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] = context.User.Identity.Name;
                }

                else
                {
                    Guid tempCartId = Guid.NewGuid();
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }

            return context.Session[CartSessionKey].ToString();
        }

        public void MigrateCart(string userName)
        {
            var shoppingCart = db.Carts.Where(c => c.CartID == ShoppingCartID);
            foreach (Cart item in shoppingCart)
            {
                item.CartID = userName;
            }

            db.SaveChanges();
        }
    }
}