using System;
using System.Linq;

namespace GUI.Windows
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class WindowAttribute : Attribute
    {
        public static bool TryGetName<T>(out string name) 
            => TryGetName(typeof(T), out name);

        public static bool TryGetName(Type t, out string name)
        {
            name = default;

            if (!(t.GetCustomAttributes(typeof(WindowAttribute), inherit: true)
                    .FirstOrDefault() is WindowAttribute attribute))
                return false;

            name = attribute.Name;
            return true;
        }

        public string Name { get; }

        public WindowAttribute(string name)
        {
            Name = name;
        }
    }
}
