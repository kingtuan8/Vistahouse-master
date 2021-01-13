using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NhomXingfa.Startup))]
namespace NhomXingfa
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
