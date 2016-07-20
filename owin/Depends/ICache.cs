using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using apistation.owin.Models;

namespace apistation.owin.Depends
{
    public interface ICache
    {
        bool HashExists(string uri, string field);
        string HashGet(string uri, string field);
        bool HashSet(string uri, EntryModel[] hashEntry);
        bool HashDelete(string uri, string field);
    }
}
