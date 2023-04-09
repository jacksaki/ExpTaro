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
            this.ImportsText = string.Join("\r\n", this.Project.Settings.Imports);

            parent.ProjectLoaded += (sender, e) =>
            {
                this.ImportsText = string.Join("\r\n", this.Project.Settings.Imports);
            };
        }
        public DbProject Project
        {
            get
            {
                return this.Parent.Project;
            }
        }
        private void SetImports()
        {
            this.Project.Settings.Imports.Clear();
            foreach(var line in this.ImportsText.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None).Where(x => !string.IsNullOrEmpty(x)))
            {
                this.Project.Settings.Imports.Add(line); 
            }
        }

        private string _ImportsText;

        public string ImportsText
        {
            get
            {
                return _ImportsText;
            }
            set
            { 
                if (_ImportsText == value)
                {
                    return;
                }
                _ImportsText = value;
                SetImports();
                RaisePropertyChanged();
            }
        }


        private LoadedAssembly _SelectedAssembly;

        public LoadedAssembly SelectedAssembly
        {
            get
            {
                return _SelectedAssembly;
            }
            set
            { 
                if (_SelectedAssembly == value)
                {
                    return;
                }
                _SelectedAssembly = value;
                RemoveSelectedAssemblyCommand.RaiseCanExecuteChanged();
                RaisePropertyChanged();
            }
        }


        private ViewModelCommand _RemoveSelectedAssemblyCommand;

        public ViewModelCommand RemoveSelectedAssemblyCommand
        {
            get
            {
                if (_RemoveSelectedAssemblyCommand == null)
                {
                    _RemoveSelectedAssemblyCommand = new ViewModelCommand(RemoveSelectedAssembly, CanRemoveSelectedAssembly);
                }
                return _RemoveSelectedAssemblyCommand;
            }
        }

        public bool CanRemoveSelectedAssembly()
        {
            return this.SelectedAssembly != null;
        }

        public void RemoveSelectedAssembly()
        {
            this.Settings.LoadedAssemblies.Remove(this.SelectedAssembly);
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
                filter.Extensions.Add("dll");
                dlg.Filters.Add(filter);
                if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    this.Settings.LoadedAssemblies.Add(new LoadedAssembly(dlg.FileName));
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
