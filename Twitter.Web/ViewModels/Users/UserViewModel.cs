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
    using Mappings;

    public class UserViewModel : IMapFrom<User>
    {

        public static Expression<Func<User, UserViewModel>> ViewModel
        {
            get
            {
                return x => new UserViewModel
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    AvatarUrl = x.AvatarUrl,
                    ContactInfo = x.ContactInfo,
                    FullName = x.FullName,
                    Summary = x.Summary,
                    Email = x.Email,
                    DateRegister = x.DateRegister,
                    FollowersCount = x.Followers.Count,
                    FollowingCount = x.Following.Count,
                    //Tweets = x.Tweets.AsQueryable().Select(TweetViewModel.ViewModel),
                    Languages = x.Languages.AsQueryable().Select(LanguageViewModel.ViewModel)
                };
            }
        }

        public string Id { get; set; }

        [Display(Name = "Username")]
        [StringLength(20, MinimumLength= 4, ErrorMessage = "The username should be at least 4 symbols long.")]
        [Remote("check", "Account", HttpMethod = "POST", ErrorMessage = "Username already taken")]
        public string UserName { get; set; }

        [Display(Name = "Fullname")]
        public string FullName { get; set; }

        public string Email { get; set; }
        public string AvatarUrl { get; set; }
        public Location Location { get; set; }
        public DateTime DateRegister { get; set; }
        public string Summary { get; set; }

        public ContactInfo ContactInfo { get; set; }
        public IEnumerable<LanguageViewModel> Languages { get; set; }

        public int FollowersCount { get; set; }

        public int FollowingCount { get; set; }
        
    }
}