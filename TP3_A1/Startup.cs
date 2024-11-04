using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TP3_A1.Startup))]
namespace TP3_A1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
