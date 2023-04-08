using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExpTaro.Models
{
    public class LoadedAssembly : AssemblyBase
    {
        public LoadedAssembly(Assembly asm) : base(asm)
        {
        }

        public override bool IsGlobal
        {
            get
            {
                return false;
            }
        }
    }
}
