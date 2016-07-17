using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace apistation.owin.Middleware
{
    using Depends;
    using Microsoft.Owin;
    using AppFunc = Func<IDictionary<string, object>, Task>;

    public class AuthMiddleware
    {
        private readonly AppFunc _next;
        private IAuth _auth;

        public AuthMiddleware(AppFunc next, IAuth auth)
        {
            if (next == null)
            {
                throw new ArgumentNullException("next");
            }

            if (auth == null)
            {
                throw new ArgumentNullException("auth");
            }

            _auth = auth;
            _next = next;
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            try
            {
                IOwinContext context = new OwinContext(environment);

                if (_auth.IsAuthenticated(context.Request) == true)
                {
                    await _next(environment); // continue
                }
                else
                {
                    // unauthorized
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("{}");
                }
            }
            catch (Exception x)
            {
                Console.WriteLine("Error in Auth Middleware");
                Console.WriteLine(x.Message);
            }
        }
    }
}
