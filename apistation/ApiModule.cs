using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apistation
{
    using Nancy;
    using Nancy.ModelBinding;
    using Nancy.Responses;
    using System.Dynamic;
    using System.Collections;
    using System.IO;
    using System.Collections.Concurrent;
    using System.Configuration;
    using apistation.Components;

    public class ApiModule : NancyModule
    {
        #region [ Components ]
        private IAuthenticationComponent Authentication { get; set; }
        private IBodyParserComponent BodyParser { get; set; }
        private IDataAccessComponent DataAccess { get; set; }
        private ILogComponent Logging { get; set; }
        private IOptionsComponent Options { get; set; }

        #endregion

        #region [ Functions ]
        #endregion

        #region [ Constructor ]
        public ApiModule()
            : base(ConfigurationManager.AppSettings["api.base.route"])
        {
            // Route Logic 
            string api_route = "/{path*}";

            this.Authentication = ComponentFactory.LoadAuthentication(new Object());
            this.BodyParser = ComponentFactory.LoadBodyParser(new Object());
            this.DataAccess = ComponentFactory.LoadDataAccess(new Object());
            this.Logging = ComponentFactory.LoadLogging(new Object());
            this.Options = ComponentFactory.LoadOptions(new Object());

            #region [ Route Handlers ]
            Get[api_route] = _ =>
            {
                // Logging 
                Console.WriteLine(String.Format("{1} {0}", Request.Path, Request.Method));
                
                try
                {
                    #region [ HTTP GET ]
                    if (this.Authentication.IsAuthenticated(Request) == true)
                    {
                        var results = this.DataAccess.SelectResources(Request.Path);

                        if (results.Any())
                        {
                            return Response.AsJson(results, HttpStatusCode.OK);
                        }

                        return Response.AsJson(results, HttpStatusCode.NotFound);
                    }
                    else
                    {
                        return Response.AsJson("not authenticated", HttpStatusCode.Unauthorized);
                    }
                    #endregion
                }
                catch (Exception x)
                {
                    this.Logging.LogException(x);
                    return Response.AsJson("error", HttpStatusCode.InternalServerError);
                }
            };

            Post[api_route] = _ =>
            {
                // Logging 
                Console.WriteLine(String.Format("{1} {0}", Request.Path, Request.Method));

                try
                {
                    #region [ HTTP POST ]
                    Hashtable response_obj = new Hashtable();
                    string resource_path = String.Format("{0}/{1}", ConfigurationManager.AppSettings["api.resource.path"], Request.Path.Replace("/", "_"));

                    if (this.Authentication.IsAuthenticated(Request))
                    {
                        List<Hashtable> model = this.BodyParser.Parse(Request);

                        this.DataAccess.Create(model);
                        return Response.AsJson(response_obj, HttpStatusCode.OK);
                    }
                    else
                    {
                        return Response.AsJson("not authenticated", HttpStatusCode.Unauthorized);
                    }

                    
                    #endregion
                }
                catch (Exception x)
                {
                    this.Logging.LogException(x);
                    return Response.AsJson("error", HttpStatusCode.InternalServerError);
                }
            };

            Put[api_route] = _ =>
            {
                // Logging 
                Console.WriteLine(String.Format("{1} {0}", Request.Path, Request.Method));

                try
                {
                    #region [ HTTP PUT ]
                    Hashtable response_obj = new Hashtable();
                    string resource_path = String.Format("{0}/{1}", ConfigurationManager.AppSettings["api.resource.path"], Request.Path.Replace("/", "_"));

                    if (this.Authentication.IsAuthenticated(Request))
                    {
                        List<Hashtable> model = this.BodyParser.Parse(Request);

                        this.DataAccess.Update(model);
                        return Response.AsJson(response_obj, HttpStatusCode.OK);
                    }
                    else
                    {
                        return Response.AsJson("not authenticated", HttpStatusCode.Unauthorized);
                    }


                    #endregion
                }
                catch (Exception x)
                {
                    this.Logging.LogException(x);
                    return Response.AsJson("error", HttpStatusCode.InternalServerError);
                }
            };

            Delete[api_route] = _ =>
            {
                // Logging 
                Console.WriteLine(String.Format("{1} {0}", Request.Path, Request.Method));

                try
                {
                    #region [ HTTP PUT ]
                    Hashtable response_obj = new Hashtable();
                    string resource_path = String.Format("{0}/{1}", ConfigurationManager.AppSettings["api.resource.path"], Request.Path.Replace("/", "_"));

                    if (this.Authentication.IsAuthenticated(Request))
                    {
                        this.DataAccess.Delete(Request.Path);
                        return Response.AsJson(response_obj, HttpStatusCode.OK);
                    }
                    else
                    {
                        return Response.AsJson("not authenticated", HttpStatusCode.Unauthorized);
                    }
                    #endregion
                }
                catch (Exception x)
                {
                    this.Logging.LogException(x);
                    return Response.AsJson("error", HttpStatusCode.InternalServerError);
                }
            };
            #endregion
        }
        #endregion
    }
}
