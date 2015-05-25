namespace Twitter.Web.Controllers
{
    using System.Web.Mvc;
    using Twitter.Data;
    using Twitter.Models;

    [Authorize(Roles = "Administrator")]

    public abstract class AdminController : BaseController
    {
        protected AdminController(ITwitterData data)
            : base(data)
        {

        }

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            this.Data.AdministrationLogs.Add(
                new AdministrationLog
                {
                    IpAddress = this.Request.UserHostAddress,
                    Url = this.Request.RawUrl,
                    UserId = this.UserProfile.Id,
                    RequestType = this.Request.RequestType,
                    PostParams = this.Request.Form.ToString(),
                });

            this.Data.SaveChanges();

            base.OnActionExecuted(filterContext);
        }
    }
}