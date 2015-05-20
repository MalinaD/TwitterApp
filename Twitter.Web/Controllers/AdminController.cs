namespace Twitter.Web.Controllers
{
    using System.Web.Mvc;

    //[Authorize(Roles = "Administrator")]

    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
    }
}