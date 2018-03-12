using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BankLedger.Startup))]
namespace BankLedger
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
