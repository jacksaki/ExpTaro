using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Livet;
namespace ExpTaro.Models
{
    public abstract class AssemblyBase: NotificationObject
    {
        protected AssemblyBase(Assembly asm) 
        {
            this.Assembly = asm;
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

        public Assembly Assembly
        {
            get;
        }
    }
}
