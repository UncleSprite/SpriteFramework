using Sprite.Common.Finder;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Sprite.Common.Dependency;
using System.IO;
using Microsoft.Extensions.DependencyModel;
using System.Linq;
using Sprite.Common.Collections;

namespace Sprite.Common.Reflection
{
    /// <summary>
    /// 应用程序目录程序集查找器
    /// </summary>
    public class AppDomainAllAssemblyFinder : FinderBase<Assembly>, IAllAssemblyFinder, ISingletonDependency
    {
        private readonly bool _filterNetAssembly;

        public AppDomainAllAssemblyFinder(bool filterNetAssembly = true)
        {
            _filterNetAssembly = filterNetAssembly;
        }

        protected override Assembly[] FindAllItems()
        {
            string[] filters =
            {
                "System",
                "Microsoft",
                "netstandard",
                "dotnet",
                "Window",
                "mscorlib",
                "api-ms-win-core"
            };
            DependencyContext context = DependencyContext.Default;
            if (context != null)
            {
                List<string> names = new List<string>();
                string[] dllName = context.CompileLibraries.SelectMany(lib => lib.Assemblies).Distinct()
                    .Select(m => m.Replace(".dll", "")).ToArray();

                if (dllName.Length > 0)
                {
                    names = (from name in dllName
                             let i = name.LastIndexOf('/') + 1
                             select name.Substring(i, name.Length - 1)).Distinct()
                             .WhereIf(name => !filters.Any(name.StartsWith), _filterNetAssembly)
                             .ToList();
                }
                else
                {
                    foreach (CompilationLibrary library in context.CompileLibraries)
                    {
                        string name = library.Name;
                        if (_filterNetAssembly && filters.Any(name.StartsWith))
                        {
                            continue;
                        }

                        if (!name.Contains(name))
                        {
                            names.Add(name);
                        }
                    }
                }
                return LoadFiles(names);
            }
            //遍历文件夹的方式，用于传统.netfx
            string path = Directory.GetCurrentDirectory();
            string[] files = Directory.GetFiles(path, "*.dll", SearchOption.TopDirectoryOnly)
                .Concat(Directory.GetFiles(path, "*.exe", SearchOption.TopDirectoryOnly))
                .ToArray();

            return files.Where(file => filters.All(token => Path.GetFileName(file)?.StartsWith(token) != true)).Select(Assembly.LoadFrom).ToArray();

        }

        private static Assembly[] LoadFiles(IEnumerable<string> files)
        {
            List<Assembly> assemblies = new List<Assembly>();
            foreach (var file in files)
            {
                AssemblyName name = new AssemblyName(file);
                try
                {
                    assemblies.Add(Assembly.Load(name));
                }
                catch (FileNotFoundException)
                {

                }
            }
            return assemblies.ToArray();
        }
    }
}
