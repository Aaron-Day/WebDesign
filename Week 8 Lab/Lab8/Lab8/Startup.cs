using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Lab8.Startup))]
namespace Lab8
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
