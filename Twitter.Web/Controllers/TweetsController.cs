namespace Twitter.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using Twitter.Data;
    using Twitter.Models;
    using Twitter.Web.ViewModels.Tweets;

    public class TweetsController : BaseController
    {
        private TwitterContext db = new TwitterContext();

        public TweetsController(ITwitterData data)
            :base(data)
        {

        }

        // GET: Tweets
        public ActionResult Index()
        {
            var tweet = this.Data.Tweets.All()
              // .Where(x => x.Author.UserName == username)
               .Select(TweetViewModel.ViewModel)
               .FirstOrDefault();

            if (tweet == null)
            {
                return this.HttpNotFound("Tweet does not exist");
            }
            else
            {
                var viewModel = new TweetViewModel
                {
                    Title = tweet.Title,
                    Description = tweet.Description,
                    TakenDate = tweet.TakenDate,
                    AuthorId = tweet.AuthorId
                };

                List<TweetViewModel> viewModelList = new List<TweetViewModel>();
                viewModelList.Add(viewModel);
                return this.View(viewModelList.ToList());
            }


        }

        //GET : Tweets/Details/id
        public ActionResult Details(int? id)
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

        // GET: Tweets/Create
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.AuthorId = new SelectList(db.Users, "Id", "FullName");

            //var model = new TweetViewModel();

           return this.View();

        }

        [HttpPost]
        public ActionResult Create(TweetViewModel model)
        {
            //return null;
            if (ModelState.IsValid)
            {
                model.AuthorId = this.User.Identity.GetUserId();
                var tweet = new Tweet() 
                {
                    AuthorId = model.AuthorId,
                    Title = model.Title, 
                    TakenDate= model.TakenDate,
                    Description = model.Description 
                };

                //db.SaveChanges();
                try
                {
                    this.UserProfile.Tweets.Add(tweet);
                    this.Data.Tweets.Add(tweet);
                    this.Data.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                    return this.RedirectToAction("Index", "Home");
                }   
                this.TempData["message"] = "Tweet added successfylly.";
                this.TempData["isMessageSuccess"] = true;

                return RedirectToAction("Index", "Home");
            }

            this.TempData["message"] = "There is a problem with the creation of this tweet. Please try again later.";
            this.TempData["isMessageSuccess"] = false;

            //ViewBag.AuthorId = new SelectList(db.Users, "Id", "FullName", model.AuthorId);
            return View("Tweets/_CreateTweet", model);
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