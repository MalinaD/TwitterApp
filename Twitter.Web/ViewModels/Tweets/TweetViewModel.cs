namespace Twitter.Web.ViewModels.Tweets
{
    using Mappings;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;
    using Twitter.Models;
    using Twitter.Web.ViewModels.Users;
    
    public class TweetViewModel :IMapFrom<Tweet>
    {
        public static Expression<Func<Tweet, TweetViewModel>> ViewModel
        {
            get
            {
                return x => new TweetViewModel
                {
                    Title = x.Title,
                    Description = x.Description,
                    Location = x.Location,
                    TakenDate = x.TakenDate,
                    AuthorId = x.Author.Id
                };
            }
        }
        public int Id { get; set; }

        public string Title { get; set; }
 
        public string Description { get; set; }

        public Location Location { get; set; }

        public DateTime TakenDate { get; set; }

        public string AuthorId { get; set; }

        public virtual User Author { get; set; }
    }
}