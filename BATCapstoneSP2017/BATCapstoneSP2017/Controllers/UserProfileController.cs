using BATCapstoneSP2017.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using BATCapstoneSP2017.ViewModels;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;


namespace BATCapstoneSP2017.Controllers
{
    public class UserProfileController : Controller
    {
        WholeContext db = new WholeContext();
        //ApplicationDbContext context = new ApplicationDbContext();
        //private ApplicationSignInManager _signInManager;
        //private ApplicationUserManager _userManager;


        //public UserProfileController()
        //{

        //}

        //public UserProfileController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        //{
        //    UserManager = userManager;
        //    SignInManager = signInManager;
        //}

        //public class ApplicationUser : IdentityUser
        //{
        //    public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        //    {
        //        // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
        //        var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
        //        // Add custom user claims here
        //        return userIdentity;
        //    }
        //}

        //public ApplicationSignInManager SignInManager
        //{
        //    get
        //    {
        //        return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
        //    }
        //    private set
        //    {
        //        _signInManager = value;
        //    }
        //}

        //public ApplicationUserManager UserManager
        //{
        //    get
        //    {
        //        //return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        //    }
        //    private set
        //    {
        //        _userManager = value;
        //    }
        //}


        
        // GET: UserProfile
        [Authorize]
        public ActionResult Index()
        {
            var user = User.Identity.Name;

            //var currentUser = db.AspNetUsers.SingleOrDefault(m => m.Email == User.Identity.Name);

            //var peeps = db.AspNetUsers.First(m => m.Email == User.Identity.Name);

            return View(db.AspNetUsers.ToList());
        }

        [Authorize]
        public ActionResult Edit(FormCollection values)
        {
            AspNetUser userProfile = new AspNetUser();
            
            //ID = User.Identity.GetUserId();
            //userProfile = 

            

            
            return View();
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> Edit(AspNetUser user)
        {
            
            //if (user.PasswordHash == null)
            //{
            //    //string userPassword;

            //    var registerLoginPasswordForUser = new RegisterViewModel();

            //    var userPassword = registerLoginPasswordForUser.Password;

            //    return View(userPassword);
            //}

            //var registerLoginPasswordForUser = new RegisterViewModel();

            //var userPassword = registerLoginPasswordForUser.Password;

            //var hashedPassword = userPassword.GetHashCode().ToString();

            //UserManager<IdentityUser> userManager = new UserManager<IdentityUser>(new UserStore(SetPasswordViewModel));
            //userManager.AddPassword();

            LoginViewModel loginInfo = new LoginViewModel();
            SetPasswordViewModel setPassword = new SetPasswordViewModel();
            RegisterViewModel registerViewModel = new RegisterViewModel();
            ApplicationUser thisUser = new ApplicationUser();
            string currentPassword = registerViewModel.Password;
            string userPassword = "Password1!";
            //string newPassword = "";
            string securityStamp = "0b379afd-3609-49a3-bde1-3c8493f4a218";
            //string securityStamp = "";

            if (ModelState.IsValid)
            {

                user.Id = User.Identity.GetUserId();
                user.Email = User.Identity.Name;
                user.UserName = User.Identity.Name;
                user.SecurityStamp = securityStamp;
                
                UserManager<IdentityUser> userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());
                var result = userManager.AddPassword(user.Id, userPassword);
                //var otherResult = userManager.ChangePassword(user.Id, currentPassword, userPassword);
                //var result = userManager.ChangePassword(user.Id, userPassword, newPassword);
                var hashedPassword = userManager.PasswordHasher.HashPassword(userPassword);
                

                //if (result.Succeeded)
                //{
                    //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    //await ApplicationSignInManager.Create()
                //}
                
                
                //string newPassword = "";    
                //user.Id = User.Identity.GetUserId();
                //user.Email = User.Identity.Name;
                //user.UserName = User.Identity.Name;
                user.PasswordHash = hashedPassword;

                //userManager.AddPassword(user.Id, newPassword);
                    
                    
                    

                    //var result = await UserManager.CreateAsync(thisUser, registerViewModel.Password);
                    //if (result.Succeeded)
                    //{
                    //    await _signInManager.SignInAsync(thisUser, isPersistent: false, rememberBrowser: false);
                    //}

                //var aUser = new ApplicationUser
                //{
                //    Email = user.Email,
                //};

                //var result = await UserManager.CreateAsync(aUser, registerViewModel.Password);
                //if (result.Succeeded)
                //{
                //    await SignInManager.SignInAsync(aUser, isPersistent:false, rememberBrowser:false);

                //    return RedirectToAction("Index", "UserProfile");

                //}

                
            
                //user.Id = User.Identity.GetUserId();
                //user.Email = User.Identity.Name;
                //user.UserName = User.Identity.Name;
                //user.PasswordHash = thisUser.PasswordHash; 
            
            
            
            
            //if (ModelState.IsValid)
            //{
                

                db.Entry(user).State = EntityState.Modified;

                db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            
            return View(user);
        }


        [Authorize]
        public ActionResult Delete(string Id = "")
        {
            AspNetUser user = new AspNetUser();

            Id = user.Id;

            
            return View(db.AspNetUsers.Find(Id));
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string Id)
        {
            AspNetUser user = new AspNetUser();

            AspNetUser userToDelete = db.AspNetUsers.Find(Id);

            db.AspNetUsers.Remove(userToDelete);
            db.SaveChanges();
            return RedirectToAction("Index");
            

        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }


    }
}