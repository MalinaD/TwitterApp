namespace Twitter.Web.Controllers
{
    using System.Web.Mvc;
    using Twitter.Data;
    using Twitter.Models;

    using System;
    using System.Linq;
    using System.Web.Routing;

    public class BaseController : Controller
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
        protected User UserProfile { get; private set; }

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            if (requestContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var username = requestContext.HttpContext.User.Identity.Name;
                var user = this.Data.Users.All().FirstOrDefault(x => x.UserName == username);
                this.UserProfile = user;
            }
            
            return base.BeginExecute(requestContext, callback, state);
        }
    }
}