
namespace Twitter.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;
    using Twitter.Models;
    using System.Data.Entity;
    using Migrations;

    public class TwitterContext : IdentityDbContext<User>, ITwitterContext
    {
        public TwitterContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TwitterContext, Configuration>());
        }

        public static TwitterContext Create()
        {
            return new TwitterContext();
        }

        public IDbSet<Discussion> Discussions { get; set; }

        public IDbSet<Tweet> Tweets { get; set; }

        public IDbSet<Trend> Trends { get; set; }
        public IDbSet<Group> Groups { get; set; }

        public IDbSet<UserLanguage> Languages { get; set; }

        public IDbSet<AdministrationLog> AdministrationLogs { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //if we need on delete action
            //modelBuilder.Entity<Endorcement>().HasRequired(x => x.UserSkill).WithMany(x => x.Endorcements).WillCascadeOnDelete(false);
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
