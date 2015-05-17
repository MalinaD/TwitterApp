namespace Twitter.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web;
    using System.Web.Mvc;
    using Twitter.Data;
    using Twitter.Web.ViewModels.Tweets;

    public class TweetsController : BaseController
    {
        public TweetsController(ITwitterData data)
            :base(data)
        {

        }

        // GET: Tweets
        public ActionResult Index()
        {
            var tweet = this.Data.Tweets.All()
               //.Where(x => x.UserName == username)
               .Select(TweetViewModel.ViewModel)
               .FirstOrDefault();

            if (tweet == null)
            {
                return this.HttpNotFound("Tweet does not exist");
            }

            return this.View(tweet);

        }
    }
}