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

    public class BaseApiModule : NancyModule
    {
        #region [ Components ]
        private IAuthenticationComponent Authentication { get; set; }
        private IBodyParserComponent BodyParser { get; set; }
        private IDataAccessComponent DataAccess { get; set; }
        private ILogComponent Logging { get; set; }
        private IOptionsComponent Options { get; set; }
        #endregion

          public BaseApiModule(
                String RouteExpression,
                IAuthenticationComponent authentication,
                IBodyParserComponent bodyParser,
                IDataAccessComponent dataAccess,
                ILogComponent log,
                IOptionsComponent options
            )
            : base(String.Format("{0}/{1}", ConfigurationManager.AppSettings["api.base.route"],RouteExpression))
        {
            // Route Logic this should be pulled from settings
            string api_route = "/{path*}";
            this.Authentication = authentication;
            this.BodyParser = bodyParser;
            this.DataAccess = dataAccess;
            this.Logging = log;
            this.Options = options;
        }
    }
}
