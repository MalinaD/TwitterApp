namespace Twitter.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class User : IdentityUser
    {
        private ICollection<Certification> certifications;
        private ICollection<UserLanguage> userLanguages;
        private ICollection<Group> groups;
        private ICollection<Trend> trends;

        public User()
        {
            this.ContactInfo = new ContactInfo();
            this.certifications = new HashSet<Certification>();
            this.userLanguages = new HashSet<UserLanguage>();
            this.groups = new HashSet<Group>();
            this.trends = new HashSet<Trend>();

        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public string FullName { get; set; }

        public string AvatarUrl { get; set; }

        public string Summary { get; set; }

        public ContactInfo ContactInfo { get; set; }

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
    }
}
