using System;
using System.Reflection;
using System.Runtime.Loader;

namespace ExpTaro.SampleDbApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AssemblyLoadContext.Default.Resolving += (AssemblyLoadContext context, AssemblyName assembly) => {
                Console.WriteLine($"Resolving {assembly.FullName}");
                return null;
            };

            foreach (Assembly assembly in AssemblyLoadContext.Default.Assemblies)
            {
                Console.WriteLine(assembly.FullName);
            }
            Console.ReadKey();
        }
    }
}