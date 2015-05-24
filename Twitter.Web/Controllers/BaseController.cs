namespace Twitter.Web.Controllers
{
    using System.Web.Mvc;
    using Twitter.Data;
    using Twitter.Models;

    using System;
    using System.Linq;
    using System.Web.Routing;
    using System.Web;

    public abstract class BaseController : Controller
    {
        private ITwitterData data;
        private User userProfile;

        protected BaseController(ITwitterData data)
        {
            this.Data = data;
        }


        protected BaseController(ITwitterData data, User userProfile)
            :this(data)
        {
            this.UserProfile = userProfile;
        }

        protected ITwitterData Data { get; private set; }
        //{
        //    get { return this.data; }
        //    private set { this.data = value; }
        //}
        protected User UserProfile { get; private set; }
        //{
        //    get { return this.userProfile; }
        //    private set { this.userProfile = value; }
        //}

        protected string ConvertImageToBase64String(HttpPostedFileBase image)
        {
            var stream = image.InputStream;
            byte[] fileBytes = new byte[stream.Length];
            int byteCount = stream.Read(fileBytes, 0, (int)stream.Length);
            string fileContent = Convert.ToBase64String(fileBytes);

            return "data:image/" + image.ContentType + ";" + "base64, " + fileContent;
        }

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            if (requestContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var username = requestContext.HttpContext.User.Identity.Name;
                var user = this.Data.Users.All()
                    .FirstOrDefault(x => x.UserName == username);

                this.UserProfile = user;
                this.ViewBag.UserProfile = user;
            }
            
            return base.BeginExecute(requestContext, callback, state);
        }

       // protected override void OnException(ExceptionContext filterContext)
       // {
       //     base.OnException(filterContext);
      //  }
    }
}