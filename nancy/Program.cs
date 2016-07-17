using Nancy;
using Nancy.Conventions;
using Nancy.Hosting.Self;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apistation
{
    public class CustomBoostrapper : DefaultNancyBootstrapper
    {
        protected override void RequestStartup(Nancy.TinyIoc.TinyIoCContainer container, Nancy.Bootstrapper.IPipelines pipelines, NancyContext context)
        {
            base.RequestStartup(container, pipelines, context);

            //CORS Enable
            pipelines.AfterRequest.AddItemToEndOfPipeline((ctx) =>
            {
                ctx.Response.WithHeader("Access-Control-Allow-Origin", "*")
                            .WithHeader("Access-Control-Allow-Methods", "POST,GET,PUT,DELETE")
                            .WithHeader("Access-Control-Allow-Headers", "Accept, Origin, Content-type");

            });
        }

        protected override void ConfigureConventions(NancyConventions conventions)
        {
            base.ConfigureConventions(conventions);
        }
    }

    public class Program
    {
        #region [ Options ]
     
        #endregion

        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            string host_listen_path = ConfigurationManager.AppSettings["host.listenpath"];

            using (var host = new NancyHost(new Uri(host_listen_path)))
            {
                host.Start();
                Console.WriteLine("Hosting at {0}", host_listen_path);
                Console.ReadLine();
            }
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine("ERROR: " + ((Exception)e.ExceptionObject).Message);
        }
    }
}
