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
    public class MenuController : Controller
    {
        WholeContext db = new WholeContext();
        //private MenuItem _db = new MenuItem();
        // Going to make new view using scaffolding
        
        
        // GET: Menu
        public ActionResult MenuItem()
        {

            //List<MenuItems> menuItems = new List<MenuItems>();
            List<MenuItem> MenuItems = new List<MenuItem>();





            return View(db.MenuItems.ToList());
            //return View(_db.MenuItem.ToList());
            //return View(_db.MovieSet.ToList());
        }

        // GET: /Home/Details/5 
        public ActionResult Details(int id)
        {

            return View();

        }

        //

        // GET: /Home/Create
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public ActionResult Create()
        {

            return View();

        }

        

        // POST: /Home/Create 
        [Authorize(Roles = "Administrator")]
        [AcceptVerbs(HttpVerbs.Post)]
        [HttpPost]
        public ActionResult Create(MenuItem menuItem)
        {

                if (ModelState.IsValid)
                {
                    db.MenuItems.Add(menuItem);
                    db.SaveChanges();
                    return RedirectToAction("MenuItem");

                }
                return View(menuItem);

            

        }

        

        // GET: /Home/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int Id = 0)
        {

            //var menuItemToEdit = (from m in db.MenuItems

                              // where m.ID == Id

                               //select m).First();
            
            return View(db.MenuItems.Find(Id));

        }

        //

        // POST: /Home/Edit/5 
        [Authorize(Roles = "Administrator")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(MenuItem menuItemToEdit)
        {

               //var originalItem = (from m in db.MenuItems

                                  //   where m.ID == menuItemToEdit.ID

                                   //  select m).First();

                /*if (!ModelState.IsValid)
                {
                    return View(originalItem);
                }*/

                //db.ApplyPropertyChanges(originalItem.EntityKey.EntitySetName, menuItemToEdit);

                if (ModelState.IsValid)
                {
                    db.Entry(menuItemToEdit).State = EntityState.Modified;

                    db.SaveChanges();

                    return RedirectToAction("MenuItem");
                }

                

                return View(menuItemToEdit);

            

        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int Id)
        {
            MenuItem menuItemToDelete = db.MenuItems.Find(Id);

            if (menuItemToDelete == null)
            {
                return HttpNotFound();
            }
            return View(menuItemToDelete);



            //return View(db.MenuItems.Find(Id));
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        //[AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DeleteConfirmed(int Id)
        {
            MenuItem menuItemToDelete = db.MenuItems.Find(Id);

           

                db.MenuItems.Remove(menuItemToDelete);
                db.SaveChanges();
                return RedirectToAction("MenuItem");
            

            //return View(menuItemToDelete);


        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Show(Image images)
        //{


        //    if (ModelState.IsValid)
        //    {
        //        using (var binaryReader = new BinaryReader(images.ImageUpload.InputStream)) 
        //            images.URL = binaryReader.ReadBytes(images.ImageUpload.ContentLength);

        //        db.Images.Add(images);
        //        db.SaveChanges();
        //        return RedirectToAction("MenuItem");
        //    }
            

        //    return View("MenuItem");
        //}



    }
}