using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace apistation.owin.Middleware
{
    using Depends;
    using Microsoft.Owin;
    using AppFunc = Func<IDictionary<string, object>, Task>;

    public class LogMiddleware
    {
        private readonly AppFunc _next;
        private ILog _log;

        public LogMiddleware(AppFunc next, ILog log)
        {
            if (next == null)
            {
                throw new ArgumentNullException("next");
            }

            if (log == null)
            {
                throw new ArgumentNullException("log");
            }

            _log = log;
            _next = next;
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            try
            {
                IOwinContext context = new OwinContext(environment);

                _log.Log(string.Format("{0} {1}", context.Request.Method, context.Request.Path.Value.ToString()));
            }
            catch(Exception x)
            {
                Console.WriteLine("Error in Log Middleware");
                Console.WriteLine(x.Message);
            }
            finally
            {
                await _next(environment); // comntinue
            }
        }
    }
}
