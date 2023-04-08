using Livet;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ExpTaro.Models
{
    public class DbProjectSettings:NotificationObject
    {
        public async Task InitializeAsync()
        {
            await System.Threading.Tasks.Task.Run(() =>
            {
                this.GlobalAssemblies.Clear();
                foreach (var asm in GlobalAssembly.GetAll())
                {
                    this.GlobalAssemblies.Add(asm);
                }
                RaisePropertyChanged(nameof(GlobalAssemblies));
            });
        }
        public DispatcherCollection<GlobalAssembly> GlobalAssemblies
        {
            get;
        } = new DispatcherCollection<GlobalAssembly>(DispatcherHelper.UIDispatcher);
        public DispatcherCollection<LoadedAssembly> LoadedAssemblies
        {
            get;
        } = new DispatcherCollection<LoadedAssembly>(DispatcherHelper.UIDispatcher);

        public DispatcherCollection<string> Imports
        {
            get;
        } = new DispatcherCollection<string>(DispatcherHelper.UIDispatcher);

    }
}
