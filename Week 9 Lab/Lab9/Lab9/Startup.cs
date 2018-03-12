using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Lab9.Startup))]
namespace Lab9
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
