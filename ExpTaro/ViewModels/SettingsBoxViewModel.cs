using ExpTaro.Models;
using Livet;
using Livet.Commands;
using Livet.EventListeners;
using Livet.Messaging;
using Livet.Messaging.IO;
using Livet.Messaging.Windows;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ExpTaro.ViewModels
{
    public class SettingsBoxViewModel : ViewModel
    {
        public delegate void ErrorOccurredEventHandler(object sender, ErrorOccurredEventArgs e);
        public event ErrorOccurredEventHandler ErrorOccurred = delegate { };
        public delegate void MessageEventHandler(object sender, MessageEventArgs e);
        public event MessageEventHandler Message = delegate { };
        public SettingsBoxViewModel(MainWindowViewModel parent) : base()
        {
            this.Parent = parent;
        }

        private bool _IsLoadingGlobalAssemblies;

        public bool  IsLoadingGlobalAssemblies
        {
            get
            {
                return _IsLoadingGlobalAssemblies;
            }
            set
            { 
                if (_IsLoadingGlobalAssemblies == value)
                {
                    return;
                }
                _IsLoadingGlobalAssemblies = value;
                RaisePropertyChanged();
            }
        }

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
                    this.Settings.LoadedAssemblies.Add(new LoadedAssembly(System.Reflection.Assembly.LoadFrom(dlg.FileName)));
                }
            }
        }
        
        public MainWindowViewModel Parent
        {
            get;
        }
        public DbProjectSettings Settings
        {
            get
            {
                return this.Parent.Project.Settings;
            }
        }
    }
}
