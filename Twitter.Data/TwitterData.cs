namespace Twitter.Data
{    
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using Twitter.Data.Repositories;
    using Twitter.Models;

    public class TwitterData : ITwitterData
    {
        private ITwitterContext context;
        private IDictionary<Type, object> repositories;

        public TwitterData(ITwitterContext context)
        {
            this.context = context;
            repositories = new Dictionary<Type, object>();
        }

        public IRepository<User> Users
        {
            get { return this.GetRepository<User>(); }
        }

        public IRepository<Certification> Certifications
        {
            get { return this.GetRepository<Certification>(); }
        }

        public IRepository<Discussion> Discussions
        {
            get { return this.GetRepository<Discussion>(); }
        }

        public IRepository<Trend> Trends
        {
            get { return this.GetRepository<Trend>(); }
        }

        public IRepository<Group> Groups
        {
            get { return this.GetRepository<Group>(); }
        }


        public IRepository<Tweet> Tweets
        {
            get { return this.GetRepository<Tweet>(); }
        }
        

        public IRepository<AdministrationLog> AdministrationLogs
        {
            get { return this.GetRepository<AdministrationLog>(); }
        } 

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var type = typeof(T);
            if (!this.repositories.ContainsKey(type))
            {
                var typeOfRepository = typeof(GenericRepository<T>);
//                if (type.IsAssignableFrom(typeof(Game)))
//                {
//                    typeOfRepository = typeof(GamesRepository);
//                }

                var repository = Activator.CreateInstance(typeOfRepository, this.context);
                this.repositories.Add(type, repository);
            }

            return (IRepository<T>)this.repositories[type];
        }
    
    }
}
