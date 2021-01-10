using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
namespace DalAPI
{
    static class Cloning
    {
        internal static T Clone<T>(this T original)where T:new()
        {
            T copy = new T();
            foreach(PropertyInfo propertyInfo in typeof(T).GetProperties())
            {
                propertyInfo.SetValue(copy, propertyInfo.GetValue(original, null), null);
            }
            return copy;
        }
    }
}
