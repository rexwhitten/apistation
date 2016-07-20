using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apistation.owin.Models
{
    public class EntryModel
    {
        public string Field { get; }

        public string Value { get; }

        public EntryModel()
        {
            Field = string.Empty;
            Value = string.Empty;
        }

        public EntryModel(string field, string valueJson)
        {
            Field = field;
            Value = valueJson;
        }
    }
}
