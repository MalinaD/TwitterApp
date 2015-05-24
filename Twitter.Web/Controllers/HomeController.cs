namespace Twitter.Web.Controllers
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using System;
    using System.Web;
    using System.Web.Mvc;
    using System.Linq;
    using System.Data.Entity;
    using AutoMapper.QueryableExtensions;
    using Twitter.Data;
    using Twitter.Models;
    using PagedList;
    using System.Collections.Generic;
    using Twitter.Web.ViewModels.Tweets;

    public class HomeController : BaseController
    {   
        public HomeController(ITwitterData data)
            :base(data)
        {

        }

        [HttpGet]
        public ActionResult Index()
        {
            if (this.UserProfile != null)
            {
                //this.ViewBag.UserName = this.UserProfile.UserName;
                ViewBag.Message = "This is my version of Twitter app maked with ASP.NET MVC";
                return this.RedirectToAction("Index", "Home");
            }
            //else
            //{
            //    return this.RedirectToAction("PageError","Home");
            //}

            //var tweets = this.Data.Tweets.All()
            //    .Include(t => t.Author)
            //    .Select(TweetViewModel.ViewModel)
            //    .OrderByDescending(t => t.TakenDate);

            return this.View();
        }

        public ActionResult About()
        {
            return this.View();
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

          [HttpGet]
          public ActionResult Tweets()
          {
              ViewBag.Message = "Tweets";
              return this.View();
          }

        [HttpGet]
        public ActionResult PageNotFound()
        {
            ViewBag.Message = "Sory, this page doesn't exist!";
            return this.View();
        }

        [HttpGet]
        public ActionResult PageError()
        {
            ViewBag.Message = "Ooops, sorry something went wrong! Please, try to connect again.";
            return this.View();
        }
    }
}