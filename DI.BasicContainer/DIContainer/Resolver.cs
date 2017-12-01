using System;
using System.Collections.Generic;
using System.Linq;

namespace DI.BasicContainer
{
    public class Resolver
    {
        private readonly Dictionary<Type, Type> _dependencyMap;

        public Resolver()
        {
            _dependencyMap = new Dictionary<Type, Type>();
        }
        public T CreateType<T>()
        {
            return (T)CreateType(typeof(T));
        }

        public void Register<TServiceType, TImplementationType>()
        {
            _dependencyMap.Add(typeof(TServiceType), typeof(TImplementationType));
        }

        private object CreateType(Type typeToResolve)
        {
            Type resolvedType = null;
            try
            {
                resolvedType = _dependencyMap[typeToResolve];
            }
            catch
            {
                throw new Exception(string.Format("Cannot resolve the type {0}", typeToResolve)); ;
            }

            var firstConstructor = resolvedType.GetConstructors().First();
            var constructorParameters = firstConstructor.GetParameters();

            // Parameterless Constructor--meaning no dependency
            if (!constructorParameters.Any()) return Activator.CreateInstance(resolvedType);

            IList<object> parameterList = new List<object>();

            foreach (var parameterToResolve in constructorParameters)
            {
                parameterList.Add(CreateType(parameterToResolve.ParameterType));  //recursively resolves dependency graph
            }

            return firstConstructor.Invoke(parameterList.ToArray());
        }
    }
}
