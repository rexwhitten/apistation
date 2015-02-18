using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apistation.Components
{
    public interface IBodyParserComponent : IApiComponent
    {
        List<System.Collections.Hashtable> Parse(Nancy.Request Request);

        List<T> Parse<T>(Nancy.Request request);

        T Parse<T>(Nancy.Request request);

    }
}
