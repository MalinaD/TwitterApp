namespace Twitter.Web.Controllers
{
    using Twitter.Data;
    using System.Linq;
    using Twitter.Web.ViewModels.Users;
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
                return this.HttpNotFound("User does not exist");
            }



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