namespace Twitter.Web.ViewModels.Tweets
{
    using Mappings;
    using System;
    using System.Linq.Expressions;
    using Twitter.Models;
    

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
                    Author = x.Author
                };
            }
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public Location Location { get; set; }

        public DateTime TakenDate { get; set; }

        public virtual User Author { get; set; }
    }
}