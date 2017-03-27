using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using BATCapstoneSP2017.Models;
using System.Web.Security;


namespace BATCapstoneSP2017.Controllers
{
    public class RolesController : Controller
    {
        // GET: Roles
        //WholeContext db = new WholeContext();
        private ApplicationDbContext context = new ApplicationDbContext();



        // GET: /Roles/
        [Authorize(Roles="Administrator")]
        public ActionResult Index()
        {
            //if (Roles.IsUserInRole("Administrator"))
            //{
            //    //var roles = db.AspNetRoles.ToList();
            //    return View(context.Roles.ToList());
            //}

            //else if (!Roles.IsUserInRole("Administrator"))
            //{
            //    return View();
            //}

            //else if (Roles.GetRolesForUser("NULL"))
            //{
            //    return View();
            //}

            //else
            //{
            //    return View();
            //}

            return View(context.Roles.ToList());
            
            
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                context.Roles.Add(new IdentityRole()
                {
                    Name = collection["RoleName"]
                });
                context.SaveChanges();
                ViewBag.ResultMessage = "Role created successfully !";
                return View("Create");
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(string RoleName)
        {
            var thisRole = context.Roles.Where(r => r.Name.Equals(RoleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            context.Roles.Remove(thisRole);
            context.SaveChanges();
            return RedirectToAction("Create");
        }

        //
        // GET: /Roles/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(string roleName)
        {
            var thisRole = context.Roles.Where(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            return View(thisRole);
        }

        //
        // POST: /Roles/Edit/5
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Microsoft.AspNet.Identity.EntityFramework.IdentityRole role)
        {
            try
            {
                context.Entry(role).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult ManageUserRoles()
        {
            // prepopulat roles for the view dropdown
            var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr =>

            new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult ManageUsers()
        {
            // prepopulat roles for the view dropdown
            var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr =>
            new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;
            return View();
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoleAddToUser(string UserName, string RoleName)
        {
            ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            manager.AddToRole(user.Id, RoleName);

            //ViewBag.ResultMessage = "Role created successfully !";

            // prepopulat roles for the view dropdown
            var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;

            return View("ManageUsers");
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetRoles(string UserName)
        {
            if (!string.IsNullOrWhiteSpace(UserName))
            {
                ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

                ViewBag.RolesForThisUser = manager.GetRoles(user.Id);

                // prepopulat roles for the view dropdown
                var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
                ViewBag.Roles = list;
            }

            return View("ManageUsers");
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRoleForUser(string UserName, string RoleName)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            if (manager.IsInRole(user.Id, RoleName))
            {
                manager.RemoveFromRole(user.Id, RoleName);
                ViewBag.ResultMessage = "Role removed from this user successfully !";
            }
            else
            {
                ViewBag.ResultMessage = "This user doesn't belong to selected role.";
            }
            // prepopulat roles for the view dropdown
            var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;

            return View("ManageUsers");
        }

    }

    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public ActionResult RoleAddToUser(string UserName, string RoleName)
    //    {
    //        ApplicationUser user = db.AspNetUsers.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
    //        var account = new AccountController();
    //        account.UserManager.AddToRole(user.Id, RoleName);
            
    //        ViewBag.ResultMessage = "Role created successfully !";
            
    //        // prepopulat roles for the view dropdown
    //        var list = db.AspNetRoles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
    //        ViewBag.Roles = list;   

    //        return View("ManageUserRoles");
    //    }

    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public ActionResult GetRoles(string UserName)
    //    {            
    //        if (!string.IsNullOrWhiteSpace(UserName))
    //        {
    //            ApplicationUser user = db.AspNetUsers.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
    //            var account = new AccountController();

    //            ViewBag.RolesForThisUser = account.UserManager.GetRoles(user.Id);

    //            // prepopulat roles for the view dropdown
    //            var list = db.AspNetRoles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
    //            ViewBag.Roles = list;            
    //        }

    //        return View("ManageUserRoles");
    //    }

    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public ActionResult DeleteRoleForUser(string UserName, string RoleName)
    //    {
    //        var account = new AccountController();
    //        ApplicationUser user = db.AspNetUsers.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

    //        if (account.UserManager.IsInRole(user.Id, RoleName))  
    //        {
    //            account.UserManager.RemoveFromRole(user.Id, RoleName);
    //            ViewBag.ResultMessage = "Role removed from this user successfully !";
    //        }
    //        else
    //        {
    //            ViewBag.ResultMessage = "This user doesn't belong to selected role.";
    //        }
    //        // prepopulat roles for the view dropdown
    //        var list = db.AspNetRoles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
    //        ViewBag.Roles = list;

    //        return View("ManageUserRoles");
    //    }
    //}
}