using Livet;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExpTaro.Models
{
    public class DbProjectSettings:NotificationObject
    {
        public ObservableCollection<Assembly> Assemblies
        {
            get;
        } = new ObservableCollection<Assembly>();
        public ObservableCollection<string> Imports
        {
            get;
        } = new ObservableCollection<string>();
    }
}
