namespace Twitter.Web.Controllers
{
    using System.Web.Mvc;
    using Twitter.Data;
    using System.Web.Mvc.Expressions;

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
            
            return this.View();
        }

        public ActionResult About()
        {
            
            return this.RedirectToAction(x => x.Contact());
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}