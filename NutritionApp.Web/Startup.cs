using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NutritionApp.Web.Startup))]
namespace NutritionApp.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
