using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apistation.Components
{
    public interface IDataAccessComponent : IApiComponent
    {
        List<Hashtable> SelectResources(string p);
    }
}
