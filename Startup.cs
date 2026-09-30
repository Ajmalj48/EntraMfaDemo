using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(EntraMfaDemo.Startup))]

namespace EntraMfaDemo
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

        private void ConfigureAuth(IAppBuilder app)
        {
            AuthenticationConfig.Configure(app);
        }
    }
}