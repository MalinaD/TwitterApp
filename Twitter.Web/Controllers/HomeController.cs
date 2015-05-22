namespace Twitter.Web.Controllers
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using System;
    using System.Web;
    using System.Web.Mvc;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Data.Entity;
    using System.IO;
    using AutoMapper.QueryableExtensions;
    using Twitter.Data;
    using Twitter.Models;
    using PagedList;
    using Twitter.Web.ViewModels;
    using System.Collections.Generic;
    using Twitter.Web.ViewModels.Tweets;

    public class HomeController : BaseController
    {
        private const int PAGE_SIZE = 10;

        public HomeController(ITwitterData data)
            :base(data)
        {

        }

        [HttpGet]
        public ActionResult Index(int? pageSize)
        {
            if (this.UserProfile != null)
            {
                //this.ViewBag.UserName = this.UserProfile.UserName;
                return this.RedirectToAction("Tweets", "Home");
            }
            ViewBag.Message = "This is my version of Twitter app maked with ASP.NET MVC";

            if (this.User.Identity.IsAuthenticated)
            {
                var tweetsFollowers = this.UserProfile
                            .Following
                            .SelectMany(m => m.Tweets)
                            .OrderByDescending(m => m.TakenDate)
                            .AsQueryable().Project().To<TweetViewModel>();

                return this.View(tweetsFollowers.ToList());
            }



            var tweets = this.Data.Tweets.All()
                .Include(t => t.Author)
                .Select(TweetViewModel.ViewModel)
                .OrderByDescending(t => t.TakenDate);

            if (tweets.Count() == null)
            {
                return this.HttpNotFound("Tweet does not exist");
            }

            int sizeOfPage = PAGE_SIZE;
            int pageNumber = pageSize ?? 1;

            PagedList<TweetViewModel> model = new PagedList<TweetViewModel>(tweets, pageNumber, sizeOfPage);

            return this.View(model.ToList());
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

        [Authorize]
        public ActionResult Tweets(int? pageSize)
        {
            ViewBag.Message = "All tweets below";

            var currentUser = this.UserProfile;
            ViewBag.CurrentUser = currentUser;
            ViewBag.UserImage = (currentUser.AvatarUrl != null) ? currentUser.AvatarUrl : "/Content/images/no-image.png";

            //to get user following list names
            List<string> userFollowing = this.UserProfile.Following
                .Select(u => u.UserName)
                .ToList();
            userFollowing.Add(currentUser.UserName);

            //to get tweets created by user's following 
            var tweetMatches = from tweet in this.Data.Tweets.All()
                               where userFollowing.Contains(tweet.Author.UserName)
                               orderby tweet.TakenDate descending
                               select tweet;

            IEnumerable<TweetViewModel> tweets = tweetMatches
                .Select(m => new TweetViewModel()
                {
                    Id = m.Id,
                    Title = m.Title,
                    Description = m.Description,
                    TakenDate = m.TakenDate,
                    AuthorId = m.Author.Id
                    
                });


            int sizeOfPage = PAGE_SIZE;
            int pageNumber = pageSize ?? 1;

            PagedList<TweetViewModel> model = new PagedList<TweetViewModel>(tweets, pageNumber, sizeOfPage);
            return this.View(model.ToList());
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