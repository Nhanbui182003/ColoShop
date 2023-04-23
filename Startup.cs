using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WeBanHang.Startup))]
namespace WeBanHang
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
