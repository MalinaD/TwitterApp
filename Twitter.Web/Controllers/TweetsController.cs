﻿namespace Twitter.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
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

            return this.View(tweet);

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
        public ActionResult Create()
        {
            ViewBag.AuthorId = new SelectList(db.Users, "Id", "FullName");
            return View();
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