using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebBrainfuck.Startup))]
namespace WebBrainfuck
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
