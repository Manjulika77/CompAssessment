using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(COMAssesment.Startup))]
namespace COMAssesment
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
