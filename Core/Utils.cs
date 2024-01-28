using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace PasswordManager.Core
{
    public static class DictFromClass<T>
    {
        public static Dictionary<string,string> GetDictFromClass(T obj)
        {
            return obj.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .ToDictionary(prop => prop.Name, prop => (string)prop.GetValue(obj, null));
        }
    }
}
