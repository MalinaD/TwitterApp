using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Twitter.Data.Startup))]
namespace Twitter.Data
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
