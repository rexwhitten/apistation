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
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Linq;
    using System.IO;
    using System.Collections.Concurrent;
    using System.Configuration;

    public class Sample
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public int number { get; set; }
    }


    public class ApiModule : NancyModule
    {
        #region [ Static Cache ]
        #endregion

        #region [ Functions ]
        /// <summary>
        /// Authentication Verification Function
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        private bool Authenticated(Request Request)
        {
            bool result = false;

            result = true;

            return result;
        }
        #endregion

        #region [ Constructor ]
        public ApiModule()
            : base(ConfigurationManager.AppSettings["api.base.route"])
        {
            string api_route = "/{path*}";

            Get[api_route] = _ =>
            {
                // Logging 
                Console.WriteLine(String.Format("{1} {0}", Request.Path, Request.Method));
                try
                {
                    #region [ HTTP GET ]
                    string path = Request.Path.Replace("/", "_");
                    string resource_path = String.Format("{0}/{1}", ConfigurationManager.AppSettings["api.resource.path"], path);

                    if (this.Authenticated(Request))
                    {
                        var resources = (new DirectoryInfo(ConfigurationManager.AppSettings["api.resource.path"]))
                                                                      .EnumerateFiles(path + "*", SearchOption.TopDirectoryOnly)
                                                                      .Select(json_file => File.ReadAllBytes(json_file.FullName));

                        int numResourcesFound = resources.Count();
                        if (numResourcesFound == 1)
                        {
                            return new Response
                            {
                                ContentType = "application/json",
                                StatusCode = HttpStatusCode.OK,
                                Contents = (stream) =>
                                {
                                    byte[] data = resources.First();
                                    stream.Write(data, 0, data.Length);
                                }
                            };
                        }
                        else if (numResourcesFound > 1)
                        {
                            return new Response
                            {
                                ContentType = "application/json",
                                StatusCode = HttpStatusCode.OK,
                                Contents = (stream) =>
                                {
                                    byte[] data;
                                    byte[] comma = Encoding.UTF8.GetBytes(",");

                                    bool first = true;
                                    foreach (var resourceData in resources)
                                    {
                                        if (first)
                                        {
                                            first = false;
                                            data = Encoding.UTF8.GetBytes("[");
                                            stream.Write(data, 0, data.Length);
                                        }
                                        else
                                            stream.Write(comma, 0, comma.Length);

                                        stream.Write(resourceData, 0, resourceData.Length);
                                    }

                                    data = Encoding.UTF8.GetBytes("]");
                                    stream.Write(data, 0, data.Length);
                                }
                            };
                        }
                    }
                    //else numResourcesFound == 0
                    return new Response
                    {
                        ContentType = "text/text",
                        StatusCode = HttpStatusCode.NotFound,
                        ReasonPhrase = Request.Path + ": not found",
                        Contents = (stream) => stream.Close()
                    };

                    #endregion
                }
                catch (Exception x)
                {
                    Console.WriteLine(String.Format("ERROR : {0}", x.Message));
                    Hashtable error_model = new Hashtable();
                    error_model.Add("error", x.Message);
                    return Response.AsJson(error_model, HttpStatusCode.InternalServerError);
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

                    if (this.Authenticated(Request))
                    {
                        if (File.Exists(resource_path))
                        {
                            // Update
                            File.WriteAllText(resource_path, Request.Body.ReadAsString());
                            response_obj.Add("resource.action", "update");
                        }
                        else
                        {
                            // Create
                            File.WriteAllText(resource_path, Request.Body.ReadAsString());
                            response_obj.Add("resource.action", "create");
                        }
                    }

                    return Response.AsJson(response_obj, HttpStatusCode.OK);
                    #endregion
                }
                catch (Exception x)
                {
                    Console.WriteLine(String.Format("ERROR : {0}", x.Message));
                    Hashtable error_model = new Hashtable();
                    error_model.Add("error", x.Message);
                    return Response.AsJson(error_model, HttpStatusCode.InternalServerError);
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

                    if (this.Authenticated(Request))
                    {
                        if (File.Exists(resource_path))
                        {
                            // Update
                            File.WriteAllText(resource_path, Request.Body.ReadAsString());
                            response_obj.Add("resource.action", "update");
                        }
                        else
                        {
                            // Create
                            File.WriteAllText(resource_path, Request.Body.ReadAsString());
                            response_obj.Add("resource.action", "create");
                        }
                    }

                    return Response.AsJson(response_obj, HttpStatusCode.OK);
                    #endregion
                }
                catch (Exception x)
                {
                    Console.WriteLine(String.Format("ERROR : {0}", x.Message));
                    Hashtable error_model = new Hashtable();
                    error_model.Add("error", x.Message);
                    return Response.AsJson(error_model, HttpStatusCode.InternalServerError);
                }
            };

            Delete[api_route] = _ =>
            {
                // Logging 
                Console.WriteLine(String.Format("{1} {0}", Request.Path, Request.Method));

                try
                {
                    #region [ HTTP DELETE ]
                    Hashtable response_obj = new Hashtable();

                    if (this.Authenticated(Request))
                    {

                    }

                    return Response.AsJson(response_obj, HttpStatusCode.OK);
                    #endregion
                }
                catch (Exception x)
                {
                    Console.WriteLine(String.Format("ERROR : {0}", x.Message));
                    Hashtable error_model = new Hashtable();
                    error_model.Add("error", x.Message);
                    return Response.AsJson(error_model, HttpStatusCode.InternalServerError);
                }
            };
        }
        #endregion
    }
}
