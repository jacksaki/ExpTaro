using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Livet;
using MessagePack;

namespace ExpTaro.Models
{
    public abstract class AssemblyBase: NotificationObject
    {
        protected AssemblyBase(Assembly asm) 
        {
            this.Assembly = asm;
            this.IsSelected = false;
        }
        protected AssemblyBase(string path)
        {
            this.Assembly = System.Reflection.Assembly.LoadFile(path);
            this.IsSelected = false;
        }

        public abstract bool IsGlobal { get; }

        private bool _IsSelected;

        public bool IsSelected
        {
            get
            {
                return _IsSelected;
            }
            set
            {
                if (_IsSelected == value)
                {
                    return;
                }
                _IsSelected = value;
                RaisePropertyChanged();
            }
        }

        public string Name
        {
            get
            {
                return this.Assembly.FullName;
            }
        }
        public Assembly Assembly
        {
            get;
        }
    }
}
