namespace Twitter.Web.ViewModels.Users
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;
    using System.Linq;
    using Twitter.Models;
    using Twitter.Web.ViewModels.Tweets;
    using Twitter.Web.ViewModels.Common;
    using System.Web.Mvc;

    public class UserViewModel
    {
        public static Expression<Func<User, UserViewModel>> ViewModel
        {
            get
            {
                return x => new UserViewModel
                {
                    UserName = x.UserName,
                    AvatarUrl = x.AvatarUrl,
                    ContactInfo = x.ContactInfo,
                    FullName = x.FullName,
                    Summary = x.Summary,
                    Email = x.Email,
                    DateRegister = x.DateRegister//,
                    //Tweets = x.Tweets.AsQueryable().Select(TweetViewModel.ViewModel),
                    //Languages = x.Languages.AsQueryable().Select(LanguageViewModel.ViewModel)
                };
            }
        }

        public string Id { get; set; }

        [Display(Name = "Username")]
        [StringLength(20, MinimumLength= 4, ErrorMessage = "The username should be at least 4 symbols long.")]
        [Remote("CheckForDuplication","Validation")]
        public string UserName { get; set; }

        [Display(Name = "Fullname")]
        public string FullName { get; set; }

        public string Email { get; set; }
        public string AvatarUrl { get; set; }
        public Location Location { get; set; }
        public DateTime DateRegister { get; set; }
        public string Summary { get; set; }

        public ContactInfo ContactInfo { get; set; }

        public ICollection<TweetViewModel> Tweets { get; set; }
        public ICollection<TweetViewModel> FavoritedTweets { get; set; }
        public ICollection<TweetViewModel> RetweetedTweets { get; set; }

        public ICollection<LanguageViewModel> Languages { get; set; }

        public virtual ICollection<UserViewModel> Followers { get; set; }

        public virtual ICollection<UserViewModel> Followings { get; set; }
        
    }
}