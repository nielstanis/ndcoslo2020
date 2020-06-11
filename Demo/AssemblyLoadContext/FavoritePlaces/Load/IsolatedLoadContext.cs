using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace FavoritePlaces.Load
{
    class IsolatedLoadContext : AssemblyLoadContext
    {
        private readonly AssemblyName[] _sharedAssemblies;
        private readonly AssemblyDependencyResolver _resolver;
        private readonly string _pluginPath;
        public IsolatedLoadContext(string pluginPath, Type[] sharedTypes)
        {
            _pluginPath = pluginPath;
            _resolver = new AssemblyDependencyResolver(pluginPath);

            _sharedAssemblies = new AssemblyName[sharedTypes.Length];
            for (int i = 0; i < sharedTypes.Length; i++)
            {
                _sharedAssemblies[i] = sharedTypes[i].Assembly.GetName();
            }
        }

        protected override Assembly Load(AssemblyName assemblyName)
        {
            //check for shared assemblies, return null because they'll be loaded by default AssemblyLoadContext 
            if (_sharedAssemblies.FirstOrDefault(x=>x.FullName == assemblyName.FullName)!=null)
            {
                return null;
            }

            string assemblyPath = _resolver.ResolveAssemblyToPath(assemblyName);
            if (assemblyPath != null)
            {
                return LoadFromAssemblyPath(assemblyPath);
            }

            throw new NotSupportedException($"Unable to load assembly: {assemblyName.Name}");
        }

        protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
        {
            string libraryPath = _resolver.ResolveUnmanagedDllToPath(unmanagedDllName);
            if (libraryPath != null)
            {
                return LoadUnmanagedDllFromPath(libraryPath);
            }

            //In case of any unmanaged dll we're unable to find we'll rely on the default AssemblyLoadContext. 
            return IntPtr.Zero;
        }
    }

}