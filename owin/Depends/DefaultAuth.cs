using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace apistation.owin.Depends
{
    public class DefaultAuth : IAuth
    {
        public DefaultAuth()
        {

        }

        public bool IsAuthenticated(IOwinRequest request)
        {
            return true;
        }
    }
}
