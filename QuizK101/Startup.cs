using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QuizK101.Startup))]
namespace QuizK101
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
