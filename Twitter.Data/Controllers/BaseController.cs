using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Twitter.Data_01.Controllers
{
    public class BaseController : Controller
    {
        protected string iTwitterData;

        public ITwitterData(){
            get{ return ITwitterData;} 
        }

        public BaseController(string ITwitterData){
            this.ITwitterData = iTwitterData;
        }

        // GET: Base
        public ActionResult Index()
        {
            return View();
        }
    }
}