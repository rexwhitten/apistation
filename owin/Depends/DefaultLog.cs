using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apistation.owin.Depends
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class DefaultLog : ILog
    {
        public DefaultLog()
        {

        }

        public void Log(Exception exception)
        {
            Console.WriteLine(string.Format("ERROR:{0}", exception.Message));
        }

        public void Log(object data)
        {
            Console.WriteLine(string.Format("{0}:{1}", DateTime.Now.ToString(), JsonConvert.SerializeObject(data)));
        }
    }
}
