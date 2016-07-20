using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apistation.owin.Depends
{
    public interface IChannel
    {
        void Handle(string uri, Action<IDictionary<string,object>> handler);
        void Emit(string uri, IDictionary<string, object> stateArg);
    }
}
