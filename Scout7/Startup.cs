using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Scout7.Startup))]
namespace Scout7
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
