using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Twitter.Models.Startup))]
namespace Twitter.Models
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
