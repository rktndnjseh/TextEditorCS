using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace firstProject
{
    internal class PluginLoader
    {
        private readonly string pluginDirectory;

        public PluginLoader(string pluginDirectory)
        {
            this.pluginDirectory = pluginDirectory;
        }

        public List<IPlugin> LoadPlugins()
        {
            var plugins = new List<IPlugin>();

            if (!Directory.Exists(pluginDirectory))
            {
                Directory.CreateDirectory(pluginDirectory);
            }

            foreach (var file in Directory.GetFiles(pluginDirectory, "*.dll"))
            {
                try
                {
                    Console.WriteLine($"Loading assembly from file: {file}");
                    var assembly = Assembly.LoadFrom(file);

                    foreach (var type in assembly.GetTypes())
                    {
                        if (typeof(IPlugin).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                        {
                            Console.WriteLine($"Found plugin type: {type.FullName}");
                            var plugin = (IPlugin)Activator.CreateInstance(type);
                            plugins.Add(plugin);
                            Console.WriteLine($"Plugin loaded: {plugin.Name}");
                        }
                    }
                }
                catch (ReflectionTypeLoadException ex)
                {
                    Console.WriteLine($"ReflectionTypeLoadException while loading {file}: {ex.Message}");
                    foreach (var loaderException in ex.LoaderExceptions)
                    {
                        Console.WriteLine($"Loader Exception: {loaderException.Message}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception while loading {file}: {ex.Message}");
                }
            }

            return plugins;
        }
    }
}
