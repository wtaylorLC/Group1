using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MovieReviews_MVC.Startup))]
namespace MovieReviews_MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
