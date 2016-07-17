using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace apistation.owin.Depends
{
    public interface IAuth
    {
        bool IsAuthenticated(IOwinRequest request);
    }
}
