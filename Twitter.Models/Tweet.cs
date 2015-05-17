namespace Twitter.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Tweet
    {
        private ICollection<Trend> trends;

        public Tweet()
        {
            this.trends = new HashSet<Trend>();
        }
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [MaxLength(140)]
        public string Description { get; set; }

        public Location Location { get; set; }

        public DateTime TakenDate { get; set; }

        [Required]
        public virtual User Author { get; set; }

        public virtual ICollection<Trend> Trends
        {
            get { return this.trends; }
            set { this.trends = value; }
        }

        //Every tweet should have favorite, retweet, report, reply and share buttons.
    }
}
