namespace Twitter.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
using System;
    using System.ComponentModel.DataAnnotations;

    public class User : IdentityUser
    {
        private ICollection<Certification> certifications;
        private ICollection<UserLanguage> userLanguages;
        private ICollection<Group> groups;
        private ICollection<Trend> trends;

        private ICollection<Tweet> tweets;
        private ICollection<Tweet> favoritedTweets;
        private ICollection<Tweet> retweetedTweets;

        private ICollection<User> followers;
        private ICollection<User> followings;




        public User()
        {
            this.followers = new HashSet<User>();
            this.followings = new HashSet<User>();
            this.ContactInfo = new ContactInfo();
            this.certifications = new HashSet<Certification>();
            this.userLanguages = new HashSet<UserLanguage>();
            this.groups = new HashSet<Group>();
            this.trends = new HashSet<Trend>();
            this.tweets = new HashSet<Tweet>();
            this.favoritedTweets = new HashSet<Tweet>();
            this.retweetedTweets = new HashSet<Tweet>();
            this.Messages = new HashSet<Message>();
        }

        public virtual ICollection<Message> Messages { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string FullName { get; set; }

        public string AvatarUrl { get; set; }

        public Location Location { get; set; }

        [MinLength(2)]
        [MaxLength(140)]
        public string Summary { get; set; }

        public ContactInfo ContactInfo { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateRegister { get; set; }

        public virtual ICollection<Certification> Certifications
        {
            get { return this.certifications; }
            set { this.certifications = value; }
        }


        public virtual ICollection<UserLanguage> Languages
        {
            get { return this.userLanguages; }
            set { this.userLanguages = value; }
        }

        [InverseProperty("Members")]
        public virtual ICollection<Group> Groups
        {
            get { return this.groups; }
            set { this.groups = value; }
        }

        public virtual ICollection<Trend> Trends
        {
            get { return this.trends; }
            set { this.trends = value; }
        }

        [InverseProperty("Author")]
        public virtual ICollection<Tweet> Tweets
        {
            get { return this.tweets; }
            set { this.tweets = value; }
        }

        public virtual ICollection<Tweet> FavoritedTweets
        {
            get { return this.favoritedTweets; }
            set { this.favoritedTweets = value; }
        }

        public virtual ICollection<Tweet> ReTweetedTweets
        {
            get { return this.retweetedTweets; }
            set { this.retweetedTweets = value; }
        }

        public virtual ICollection<User> Followers
        {
            get { return this.followers; }
            set { this.followers = value; }
        }

        public virtual ICollection<User> Following
        {
            get { return this.followings; }
            set { this.followings = value; }
        }

        public class Message
        {
            public int Id { get; set; }
            public string MessageText { get; set; }
            public DateTime DateCreated { get; set; }
        }
    }
}
