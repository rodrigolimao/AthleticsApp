using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AtleticaEcoUff.Startup))]
namespace AtleticaEcoUff
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
