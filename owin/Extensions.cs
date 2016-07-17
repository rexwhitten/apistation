using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apistation.owin
{
    using System.Collections;
    using System.Diagnostics;

    public static class Extensons
    {
        static string GetDeepMessage(Exception e)
        {
            if(e.InnerException != null)
            {
                return GetDeepMessage(e.InnerException);
            }

            return e.Message;
        }

        public static Hashtable ToHashtable(this Exception exp)
        {
            var h = new Hashtable();

            h.Add("message", GetDeepMessage(exp));
            
            return h;
        }
    }
}
