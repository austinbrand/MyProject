using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BATCapstoneSP2017.Startup))]
namespace BATCapstoneSP2017
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
