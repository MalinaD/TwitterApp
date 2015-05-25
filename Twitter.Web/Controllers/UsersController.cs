namespace Twitter.Web.Controllers
{
    using Twitter.Data;
    using System.Linq;
    using Twitter.Web.ViewModels.Users;
    using InputModels;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Twitter.Web.ViewModels.Tweets;
    using System.Web.Mvc;
    using System;
    using Microsoft.AspNet.Identity;

    [Authorize]
    public class UsersController : BaseController
    {

        private const int PAGE_SIZE = 10;

        public UsersController(ITwitterData data)
            :base(data)
        {

        }

        // GET: Users/{username}
        [HttpGet]
        public ActionResult Index(string username)
        {
            if (String.IsNullOrEmpty(username) && User.Identity.IsAuthenticated)
            {
                username = User.Identity.GetUserName();
            }

            var user = this.Data.Users
               .All()
               .Where(u => u.UserName == username)
               .Select(UserViewModel.ViewModel)
               .FirstOrDefault();


                if (username == null)
                {
                    //return this.HttpNotFound("User does not exist");
                    return this.RedirectToAction("PageNotFound", "Home");
                }
            

            ViewBag.CurrentUser = user;
            ViewBag.UserViewingThePageName = this.UserProfile.UserName;
            ViewBag.UserViewingThePageId = this.UserProfile.Id;

            //ViewBag.DisplayButtons = false;
           // ViewBag.UserIsFollowing = false;
          //ViewBag.DisplayButtons = (this.UserProfile.Id != userProfile.Id);

                //TODO following users

                //List<string> listOfFollowingUsers = this.UserProfile.Following.Select(f => f.UserName)
                //    .ToList();

                //bool userIsFollowing = listOfFollowingUsers.Contains(username) ? true : false;
                
                //ViewBag.UserIsFollowing = userIsFollowing;

            //var tweets = this.Data.Tweets.All()
            //    .Where(t => t.AuthorId == user.Id)
            //    .Select(TweetViewModel.ViewModel)
            //    .OrderByDescending(t => t.TakenDate);

            return this.View(user);
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

        [HttpGet]
        public ActionResult Edit()
        {
            var inputModel = EditUserInputModel.FromModel(this.UserProfile);
            return this.View(inputModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditUserInputModel model)
        {
            if (this.ModelState.IsValid)
            {
                var updatedUser = model.UpdateUser(this.UserProfile);
                this.Data.Users.Update(updatedUser);
                this.Data.SaveChanges();

                //this.TempData[SystemMessageType.Information.ToString()] = "Profile updated";
                return this.RedirectToAction("Index", "Home");
            }

            return this.View(model);
        }
    }
}