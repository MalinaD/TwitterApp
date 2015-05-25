namespace Twitter.Web.Controllers
{
    using Microsoft.AspNet.Identity;
    using System;
    using System.Web.Mvc;
    using System.Linq;
    using System.Data.Entity;
    using Twitter.Models;
    using Twitter.Data;
    using Twitter.Web.ViewModels.Tweets;
    using Twitter.Web.ViewModels;
    using System.Net;
    using System.Collections.Generic;

    [Authorize]
    public class TweetsController : BaseController
    {
        private TwitterContext db = new TwitterContext();

        // GET: Tweets
        public TweetsController(ITwitterData data)
            :base(data)
        {

        }

        [HttpGet]
        public ActionResult New()
        {
            return View();
        }
        
        public ActionResult Index()
        {
        //   // var tweets = this.Data.Tweets
        //    //    .Select(TweetViewModel.ViewModel)
        //   //     .OrderByDescending(t => t.TakenDate);
        //    //.Include(t => t.AuthorId); //.All()
        //        //.Include(t  => t.Author)
        //        //.Select(TweetViewModel.ViewModel)
        //        //.OrderByDescending(t => t.TakenDate);

        //    //if (tweets == null)
        //    //{
        //    //    return this.RedirectToAction("PageNotFound", "Home");
                
        //    //}

        //    //return this.View(tweets);

        //    ViewBag.Message = "Tweets";
        //    return this.View();

             List<Tweet> tweets = new List<Tweet>();

            if (this.UserProfile != null)
            {
                if (this.UserProfile.Following.Count <= 0)
                {
                    tweets = this.Data.Tweets.All().ToList();
                }
                else
                {
                    foreach (var user in this.UserProfile.Following)
                    {
                        tweets.AddRange(user.Tweets);
                    }
                }
            }
            else
            {
                tweets = this.Data.Tweets.All().ToList();
            }

            return View(tweets);
        }

        //GET : Tweets/Details/id
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var tweet = db.Tweets.Find(id);
                //(from tweet in db.Tweets
                //          where tweet.Id == id
                //         select tweet).Take(1).ToList();
                

            if (tweet == null)
            {
                return RedirectToAction("PageNotFound","Home");
            }

            return View(tweet);
        }

        // GET: Tweets/Create
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.AuthorId = new SelectList(db.Users, "Id", "FullName");

            //var model = new TweetViewModel();

           return this.View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TweetViewModel model)
        {
            //return null;
            if (this.ModelState.IsValid)
            {
                model.AuthorId = this.User.Identity.GetUserId();
                db.Tweets.Add(new Tweet()
                {
                    //AuthorId = model.AuthorId,
                    Title = model.Title,
                    Description = model.Description,
                    TakenDate = DateTime.Now
                });

                    db.SaveChanges();
   
                this.TempData["message"] = "Tweet added successfylly.";
                this.TempData["isMessageSuccess"] = true;

                return RedirectToAction("Index", "Home");
            }

            this.TempData["message"] = "There is a problem with the creation of this tweet. Please try again later.";
            this.TempData["isMessageSuccess"] = false;

            this.ViewBag.AuthorId = new SelectList(db.Users, "Id", "FullName", model.AuthorId);
            return View("Tweets/Create", model);
        }

        public ActionResult Report(int tweetId)
        {
            var tweet = this.Data.Tweets.Find(tweetId);
            if (tweet == null)
            {

            }

            tweet.Reports++;
            this.Data.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        // GET: Tweets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Tweet tweet = db.Tweets.Find(id);
            if (tweet == null)
            {
                return HttpNotFound();
            }

            ViewBag.AuthorId = new SelectList(db.Users, "Id", "FullName", tweet.Author.Id);
            return View(tweet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AuthorId,Description,TakenDate")] Tweet tweet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tweet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AuthorId = new SelectList(db.Users, "Id", "FullName", tweet.AuthorId);

            return View(tweet);
        }

        // GET: Tweets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Tweet tweet = db.Tweets.Find(id);

            if (tweet == null)
            {
                return HttpNotFound();
            }

            return View(tweet);
        }

    }
}