namespace Twitter.Web.Controllers
{
    using Twitter.Data;
    using System.Linq;
    using Twitter.Web.ViewModels.Users;
    using Twitter.Web.ViewModels.Tweets;
    using System.Web.Mvc;
    using System;
    using Microsoft.AspNet.Identity;
    using System.Collections.Generic;
    using PagedList;

    [Authorize]
    public class UsersController : BaseController
    {

        private const int PAGE_SIZE = 10;

        public UsersController(ITwitterData data)
            :base(data)
        {

        }
        // GET: Users
        public ActionResult Index(string username, int? pageSize)
        {
            if (String.IsNullOrEmpty(username) && User.Identity.IsAuthenticated)
            {
                username = User.Identity.GetUserName();
            }

            var userProfile = this.Data.Users.All()
               .Where(x => x.UserName == username)
               .Select(UserViewModel.ViewModel)
               .FirstOrDefault();

            if (userProfile == null)
            {
                //return this.HttpNotFound("User does not exist");
                return this.RedirectToAction("PageNotFound", "Home");
            }

            ViewBag.CurrentUser = userProfile;
            //ViewBag.DisplayButtons = false;
           // ViewBag.UserIsFollowing = false;

            if (User.Identity.IsAuthenticated)
            {
                ViewBag.DisplayButtons = (this.UserProfile.Id != userProfile.Id);

                //TODO following users

                List<string> listOfFollowingUsers = this.UserProfile.Following.Select(f => f.UserName)
                    .ToList();

                bool userIsFollowing = listOfFollowingUsers.Contains(username) ? true : false;
                
                ViewBag.UserIsFollowing = userIsFollowing;
            }

            var userTweets = this.Data.Tweets.All()
               .Select(TweetViewModel.ViewModel)
               .Where(t => t.AuthorId == userProfile.Id)
               .OrderByDescending(t => t.TakenDate);

            int sizeOfPage = PAGE_SIZE;
            int pageNumber = pageSize ?? 1;

            PagedList<TweetViewModel> model = new PagedList<TweetViewModel>(userTweets, pageNumber, sizeOfPage);
            return this.View(model);
        }

        public ActionResult MyProfile()
        {
            return this.Redirect("/User/mimi");
        }

        [OutputCache(Duration = 60)]
        [Authorize]
        public ActionResult GetParam(string name)
        {
            return Content(string.Format("{0} - {1}", name, DateTime.Now));
        }
    }
}