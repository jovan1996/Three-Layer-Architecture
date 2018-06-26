using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(jovan_somborski_63_15.Startup))]
namespace jovan_somborski_63_15
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
