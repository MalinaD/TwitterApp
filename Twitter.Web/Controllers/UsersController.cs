namespace Twitter.Web.Controllers
{
    using Twitter.Data;
    using System.Linq;
    using Twitter.Web.ViewModels.Users;
    using Twitter.Web.ViewModels.Tweets;
    using System.Web.Mvc;
    using System;

    [Authorize]
    public class UsersController : BaseController
    {
        public UsersController(ITwitterData data)
            :base(data)
        {

        }
        // GET: Users
        public ActionResult Index(string username)
        {
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
            }

            var userTweets = this.Data.Tweets.All()
               .Select(TweetViewModel.ViewModel)
               .Where(t => t.AuthorId == userProfile.Id)
               .OrderByDescending(t => t.TakenDate);

            return this.View(userProfile);
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