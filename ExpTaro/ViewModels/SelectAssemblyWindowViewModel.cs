using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using Livet;
using Livet.Commands;
using Livet.Messaging;
using Livet.Messaging.IO;
using Livet.EventListeners;
using Livet.Messaging.Windows;
using ExpTaro.Models;
using System.Windows.Input;
using System.Reflection;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace ExpTaro.ViewModels
{
    public class SelectAssemblyWindowViewModel : ViewModel
    {
        public SelectAssemblyWindowViewModel(DbProjectSettings settings) : base()
        {
            
        }
        public void Initialize()
        {
        }

        public DispatcherCollection<Assembly> LoadedAssemblies
        {
            get;
        } = new DispatcherCollection<Assembly>(DispatcherHelper.UIDispatcher);

        public DispatcherCollection<GlobalAssembly> GlobalAssemblies
        {
            get;
        } = new DispatcherCollection<GlobalAssembly>(DispatcherHelper.UIDispatcher);

        private ViewModelCommand _AddAssemblyCommand;

        public ViewModelCommand AddAssemblyCommand
        {
            get
            {
                if (_AddAssemblyCommand == null)
                {
                    _AddAssemblyCommand = new ViewModelCommand(AddAssembly);
                }
                return _AddAssemblyCommand;
            }
        }

        public void AddAssembly()
        {
            using (var dlg = new CommonOpenFileDialog())
            {
                dlg.DefaultExtension = ".dll";
                var filter = new CommonFileDialogFilter()
                {
                    DisplayName = ".NET アセンブリ(*.dll)",
                };
                filter.Extensions.Add("cs");
                dlg.Filters.Add(filter);
                if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    this.LoadedAssemblies.Add(System.Reflection.Assembly.LoadFrom(dlg.FileName));
                }
            }
        }
    }
}
