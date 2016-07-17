using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(apistation.owin.DefaultStartup))]

namespace apistation.owin
{
    public class DefaultStartup
    {
        private readonly string _baseUrl;

        public DefaultStartup(string baseUrl)
        {
            _baseUrl = baseUrl;
        }
        public void Configuration(IAppBuilder app)
        {
            app.UseWelcomePage(new Microsoft.Owin.Diagnostics.WelcomePageOptions()
            {
                Path = new PathString("/welcome")
            });
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            string baseUrl = "http://127.0.0.1:9908";
            using (WebApp.Start<DefaultStartup>(baseUrl))
            {
                Console.WriteLine("Ready.");
                Console.ReadKey();
            }
        }
    }
}
