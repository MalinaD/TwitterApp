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


        public UsersController(ITwitterData data)
            :base(data)
        {

        }

        // GET: Users/{username}
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            var users = this.Data.Users.All().ToList();

            return View(users);
        }

        [HttpGet]
        public ActionResult ViewProfile()
        {
            var currentUser = this.UserProfile;

            var tweets = currentUser.Tweets.ToList();

            return this.View(tweets);
        }

        [AllowAnonymous]
        [HttpGet]
        public JsonResult IsValid(string username)
        {
            if (this.Data.Users.All().FirstOrDefault(u => u.UserName.Equals(username)) != null)
            {
                return this.Json(false, JsonRequestBehavior.AllowGet);
            }

            return this.Json(true, JsonRequestBehavior.AllowGet);
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