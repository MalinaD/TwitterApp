namespace Twitter.Web.Controllers
{
    using System.Web.Mvc;
    using System.Web;
    using System.Linq;
    using System.Linq.Expressions;
    using Twitter.Data;

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

        public ActionResult Tweets()
        {
            ViewBag.Message = "All tweets below";

            return this.View();
        }
    }
}