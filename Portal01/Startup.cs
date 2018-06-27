using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ForumIT.Startup))]
namespace ForumIT
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
