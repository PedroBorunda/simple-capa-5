using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IssuesGithub.Startup))]
namespace IssuesGithub
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
