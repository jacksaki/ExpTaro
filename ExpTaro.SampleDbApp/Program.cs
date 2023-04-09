using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace ExpTaro.SampleDbApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var hoge = new AppDbContext().Users.AsNoTracking().ToList();
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