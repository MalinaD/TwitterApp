namespace Twitter.Data
{
    using System.Data.Entity;
    using Twitter.Models;

    public interface ITwitterContext
    {
         IDbSet<Certification> Certifications { get; set; }

         IDbSet<Discussion> Discussions { get; set; }

         IDbSet<Tweet> Tweets { get; set; }

         IDbSet<Trend> Trends { get; set; }
         IDbSet<Group> Groups { get; set; }

         IDbSet<UserLanguage> Languages { get; set; }

         IDbSet<AdministrationLog> AdministrationLogs { get; set; }

         int SaveChanges();

    }
}
