using Microsoft.Owin;
using Owin;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(apistation.owin.ApiStartup))]
namespace apistation.owin
{
    using Depends;
    using Middleware;
    using Models;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class ApiStartup
    {
        private readonly string _baseUrl;
        private static IConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");

        #region Constructors
        /// <summary>
        /// api startup ctor
        /// </summary>
        /// <param name="baseUrl"></param>
        public ApiStartup(string baseUrl)
        {
            _baseUrl = baseUrl;
        }
        #endregion

        public void Configuration(IAppBuilder app)
        {
            app.UseWelcomePage(new Microsoft.Owin.Diagnostics.WelcomePageOptions()
            {
                Path = new PathString("/welcome")
            });

            // Composition of dependecies
            ObjectFactory.Register<ICache, RedisCache>();

            app.Use(typeof(LogMiddleware), new DefaultLog());
            app.Use(typeof(AuthMiddleware), new DefaultAuth());
            
            

            // handles all api requests
            app.Run(context =>
            {
                ICache cache = ObjectFactory.Resolve<ICache>();
                var channel = redis.GetSubscriber();
                var body = "{}"; // default body ; kept minimal

                context.Response.StatusCode = 404; // default status code
                context.Response.Headers.Add("Content-Type", new string[] { "application/json" });

                try
                {
                    switch (context.Request.Method)
                    {
                        case "GET":
                            #region http:get
                            if (cache.HashExists(context.Request.Path.Value, "@body"))
                            {
                                body = cache.HashGet(context.Request.Path.Value, "@body");
                                context.Response.StatusCode = 200;
                            }
                            #endregion
                            break;
                        case "POST":
                            #region http:post
                            if (!cache.HashExists(context.Request.Path.Value, "@body"))
                            {
                                JObject input = context.Request.Body.ToJObject();
                                cache.HashSet(context.Request.Path.Value, new EntryModel[3] {
                                    new EntryModel("@body", input.ToString()),
                                    new EntryModel("@event:create", DateTime.Now.ToString()),
                                    new EntryModel("@uuid",Guid.NewGuid().ToString())
                                });
                                context.Response.StatusCode = 202;
                            }
                            else
                            {
                                context.Response.StatusCode = 400;
                            }
                            #endregion
                            break;
                        case "PUT":
                            #region http:put
                            if (cache.HashExists(context.Request.Path.Value, "@body"))
                            {
                                JObject input = context.Request.Body.ToJObject();
                                cache.HashSet(context.Request.Path.Value, new EntryModel[3] {
                                    new EntryModel("@body", input.ToString()),
                                    new EntryModel("@event:create", DateTime.Now.ToString()),
                                    new EntryModel("@uuid",Guid.NewGuid().ToString())
                                });
                                context.Response.StatusCode = 202;
                            }
                            #endregion
                            break;
                        case "DELETE":
                            #region http:delete
                            if (cache.HashExists(context.Request.Path.Value, "@body"))
                            {
                                body = JsonConvert.SerializeObject(cache.HashDelete(context.Request.Path.Value, "@body"));
                                context.Response.StatusCode = 200;
                            }
                            #endregion
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception error)
                {
                    context.Response.StatusCode = 500;
                    body = JsonConvert.SerializeObject(error.ToHashtable());
                }
                return context.Response.WriteAsync(body);
            });
        }
    }
}
