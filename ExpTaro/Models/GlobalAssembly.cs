using Livet;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExpTaro.Models
{
    public class GlobalAssembly:AssemblyBase
    {
        public GlobalAssembly(Assembly asm):base(asm)
        {
        }

        public override bool IsGlobal
        {
            get
            {
                return true;
            }
        }

        private static void LoadDefaultAssemblies()
        {

        }
        public static IEnumerable<GlobalAssembly> GetAll()
        {
            var asm = typeof(Npgsql.EntityFrameworkCore.PostgreSQL.NpgsqlRetryingExecutionStrategy).Assembly;
            var globalAssemblies = new Lazy<List<GlobalAssembly>>(() =>
            {
                return AssemblyLoadContext.Default.Assemblies
                    .Select(a => new GlobalAssembly(a)).ToList();
            });
            return globalAssemblies.Value.OrderBy(x=>x.Assembly.FullName);
        }

    }
}
