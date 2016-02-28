using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebPaintProject.Startup))]
namespace WebPaintProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
