using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;
using Microsoft.Owin;
using Owin;
using StackExchange.Redis;
using System.IO;
using NetJSON;
using System.Collections;

[assembly: OwinStartup(typeof(apistation.owin.DefaultStartup))]

namespace apistation.owin
{
    public static class Extensions
    {
        public static string ReadAsString(this Stream stream)
        {
            // convert stream to string
            StreamReader reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
    }

    public class DefaultStartup
    {
        private readonly string _baseUrl;
        private static IConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");

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

            app.Run(context =>
            {
                // HTTP SERVE
                var cache = redis.GetDatabase();
                context.Response.StatusCode = 404;
                var body = "{}";

                try
                {
                    switch (context.Request.Method)
                    {
                        case "GET":
                            if (cache.HashExists(context.Request.Path.Value, "@body"))
                            {
                                body = cache.HashGet(context.Request.Path.Value, "@body").ToString();
                                context.Response.StatusCode = 200;
                            }
                            break;
                        case "POST":
                            if (cache.HashExists(context.Request.Path.Value, "@body"))
                            {
                                object inputBody = NetJSON.NetJSON.DeserializeObject(context.Request.Body.ReadAsString());
                                string inputJson = NetJSON.NetJSON.Serialize(inputBody);
                                cache.HashSet(context.Request.Path.Value, new HashEntry[1] {
                                    new HashEntry("@body", inputJson)
                                });
                                body = NetJSON.NetJSON.Serialize(true);
                                context.Response.StatusCode = 202;
                            }
                            else
                            {
                                context.Response.StatusCode = 400;
                            }
                            break;
                        case "PUT":
                            if (cache.HashExists(context.Request.Path.Value, "@body"))
                            {
                                object inputBody = NetJSON.NetJSON.DeserializeObject(context.Request.Body.ReadAsString());
                                string inputJson = NetJSON.NetJSON.Serialize(inputBody);
                                cache.HashSet(context.Request.Path.Value, new HashEntry[1] {
                                    new HashEntry("@body", inputJson)
                                });
                                body = NetJSON.NetJSON.Serialize(true);
                                context.Response.StatusCode = 202;
                            }
                            break;
                        case "DELETE":
                            if (cache.HashExists(context.Request.Path.Value, "@body"))
                            {
                                body = NetJSON.NetJSON.Serialize(cache.HashDelete(context.Request.Path.Value, "@body"));
                                context.Response.StatusCode = 200;
                            }
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception)
                {
                    context.Response.StatusCode = 500;
                }

                return context.Response.WriteAsync(body);
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
