namespace Twitter.Web.Controllers
{
    using System.Web.Mvc;
    using System.Web;
    using Twitter.Data;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using System;
    using Twitter.Models;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class HomeController : BaseController
    {
        public HomeController(ITwitterData data)
            :base(data)
        {

        }
        public ActionResult Index()
        {
            //if (this.UserProfile != null)
           // {
           //     this.ViewBag.UserName = this.UserProfile.UserName;
           // }
            ViewBag.Message = "This is my version of Twitter app maked with ASP.NET MVC";
            return this.View();
        }

        public ActionResult About()
        {
            return this.View();
            //return this.RedirectToAction(x => x.Contact());
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult AdminPage()
        {
            ViewBag.Message = "For administrators only: welcome";

            return View();
        }

          [Authorize(Roles = "Administrator")]
        public ActionResult CreateAdminRole()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new TwitterContext()));
            var roleCreateResult = roleManager.Create(new IdentityRole("Administrator"));

            if (!roleCreateResult.Succeeded)
            {
                throw new Exception(string.Join("; ", roleCreateResult.Errors));
            }

            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = new User() { UserName = "admin", Email = "admin@admin.com",DateRegister = DateTime.Now };
            var createUserResult = userManager.Create(user, "Admin123!");

            if (!createUserResult.Succeeded)
            {
                throw new Exception(string.Join("; ", createUserResult.Errors));
            }

            userManager.AddToRole(user.Id, "Administrator");

            return View();

        }

        public ActionResult Tweets()
        {
            ViewBag.Message = "All tweets below";

            return this.View();
        }
    }
}