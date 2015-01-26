using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apistation.Components
{
    public class ComponentFactory
    {
        internal static IAuthenticationComponent LoadAuthentication(IAuthenticationComponent p)
        {
            throw new NotImplementedException();
        }

        internal static IBodyParserComponent LoadBodyParser(IBodyParserComponent p)
        {
            throw new NotImplementedException();
        }

        internal static IDataAccessComponent LoadDataAccess(IDataAccessComponent p)
        {
            throw new NotImplementedException();
        }

        internal static ILogComponent LoadLogging(ILogComponent p)
        {
            throw new NotImplementedException();
        }

        internal static IOptionsComponent LoadOptions(IOptionsComponent p)
        {
            throw new NotImplementedException();
        }
    }
}
