using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PoCIntegracaoSEI.Startup))]
namespace PoCIntegracaoSEI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
