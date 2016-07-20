using System;
using apistation.owin.Depends;
using System.Collections.Generic;

namespace apistation.owin
{
    internal class ObjectFactory
    {
        static Dictionary<Type, Type> _registry = new Dictionary<Type, Type>();

        internal static TInterface Resolve<TInterface>()
        {
            return (TInterface)Activator.CreateInstance(_registry[typeof(TInterface)], null);
        }

        internal static void Register<TInterface, TImplementation>() where TImplementation : TInterface
        {
            if (_registry.ContainsKey(typeof(TInterface)))
            {
                _registry.Add(typeof(TInterface), typeof(TImplementation));
            }
            else
            {
                _registry[typeof(TInterface)] = typeof(TImplementation);
            }
        }
    }
}