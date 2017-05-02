using BATCapstoneSP2017.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.IO;

namespace BATCapstoneSP2017.Controllers
{
    public class OrderMenuItemsController : Controller
    {
        WholeContext db = new WholeContext();
        
        
        [Authorize(Roles = "Administrator")]
        // GET: OrderMenuItems
        public ActionResult Index()
        {
            return View(db.Orders.ToList());
        }


        //public ActionResult OrderNow()
        //{
        //    return View();
        //}

        [Authorize]
        public ActionResult CreateOrder()
        {
            return View();
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        [HttpPost]
        public ActionResult CreateOrder(Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(order);
        }

        [Authorize]
        public ActionResult Edit(int Id = 0)
        {

            //var menuItemToEdit = (from m in db.MenuItems

            // where m.ID == Id

            //select m).First();

            return View(db.Orders.Find(Id));

        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(Order orderItemToEdit)
        {

            

            if (ModelState.IsValid)
            {
                db.Entry(orderItemToEdit).State = EntityState.Modified;

                db.SaveChanges();

                return RedirectToAction("Index");
            }



            return View(orderItemToEdit);



        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int Id)
        {
            Order orderItemToDelete = db.Orders.Find(Id);

            if (orderItemToDelete == null)
            {
                return HttpNotFound();
            }
            return View(orderItemToDelete);



            //return View(db.MenuItems.Find(Id));
        }


        [Authorize (Roles = "Administrator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        //[AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DeleteConfirmed(int Id)
        {
            Order orderItemToDelete = db.Orders.Find(Id);



            db.Orders.Remove(orderItemToDelete);
            db.SaveChanges();
            return RedirectToAction("Index");


            //return View(menuItemToDelete);


        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }


    }
}