using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(B2CWebTemplate.Startup))]
namespace B2CWebTemplate
{
    public partial class Startup
    {
      
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
