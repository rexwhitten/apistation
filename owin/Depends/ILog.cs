using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apistation.owin.Depends
{
    public interface ILog
    {
        void Log(object data);

        void Log(Exception exception);
    }
}
