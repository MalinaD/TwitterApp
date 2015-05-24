namespace Twitter.Data
{
    using Twitter.Data.Repositories;
    using Twitter.Models;

    public interface ITwitterData
    {
        IRepository<User> Users { get; }


        IRepository<Discussion> Discussions { get; }

        IRepository<Trend> Trends { get; }

        IRepository<Group> Groups { get; }

        IRepository<Tweet> Tweets { get; }

        IRepository<AdministrationLog> AdministrationLogs { get; }

        int SaveChanges();
    }
}
