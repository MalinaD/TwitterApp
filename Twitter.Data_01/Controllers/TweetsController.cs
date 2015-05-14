using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Twitter.Data.Controllers
{
    public class TweetsController : Controller
    {
        // GET: Tweet
        public ActionResult Index()
        {
            return View();
        }
    }
}