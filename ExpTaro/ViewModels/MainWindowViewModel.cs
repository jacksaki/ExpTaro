using ExpTaro.Models;
using Livet;
using Livet.Commands;
using Livet.EventListeners;
using Livet.Messaging;
using Livet.Messaging.IO;
using Livet.Messaging.Windows;
using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace ExpTaro.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        public event EventHandler ProjectLoaded = delegate { };

        public void Initialize()
        {

        }
        public DbProject Project
        {
            get;
            private set;
        }
        
        public MainWindowViewModel() : base()
        {
            this.DialogCoordinator = MahApps.Metro.Controls.Dialogs.DialogCoordinator.Instance;
            LoadProject();
            this.Project.PropertyChanged += (sender, e) => { RaisePropertyChanged(nameof(Project)); };
            this.DbContextBoxViewModel = new DbContextBoxViewModel(this);
            this.DbContextBoxViewModel.ErrorOccurred += ViewModel_ErrorOccurred;
            this.DbContextBoxViewModel.Message += ViewModel_Message;
            this.TablesBoxViewModel = new TablesBoxViewModel(this);
            this.TablesBoxViewModel.ErrorOccurred += ViewModel_ErrorOccurred;
            this.TablesBoxViewModel.Message += ViewModel_Message;

            this.SettingsBoxViewModel = new SettingsBoxViewModel(this);
            this.SettingsBoxViewModel.ErrorOccurred += ViewModel_ErrorOccurred;
            this.SettingsBoxViewModel.Message += ViewModel_Message;

            this.ExplainBoxViewModel = new ExplainBoxViewModel(this);
            this.ExplainBoxViewModel.ErrorOccurred += ViewModel_ErrorOccurred;
            this.ExplainBoxViewModel.Message += ViewModel_Message;
        }

        private void ViewModel_Message(object sender, MessageEventArgs e)
        {
            this.MessageQueue.Enqueue(e.Message);
        }


        private ViewModelCommand _SaveProjectCommand;

        public ViewModelCommand SaveProjectCommand
        {
            get
            {
                if (_SaveProjectCommand == null)
                {
                    _SaveProjectCommand = new ViewModelCommand(SaveProject);
                }
                return _SaveProjectCommand;
            }
        }

        public void SaveProject()
        {
            this.Project.Save();
        }


        private ViewModelCommand _LoadProjectCommand;

        public ViewModelCommand LoadProjectCommand
        {
            get
            {
                if (_LoadProjectCommand == null)
                {
                    _LoadProjectCommand = new ViewModelCommand(LoadProject);
                }
                return _LoadProjectCommand;
            }
        }

        public void LoadProject()
        {
            this.Project = DbProject.Load();
            this.ProjectLoaded(this, EventArgs.Empty);
        }


        public MaterialDesignThemes.Wpf.SnackbarMessageQueue MessageQueue
        {
            get;
        } = new MaterialDesignThemes.Wpf.SnackbarMessageQueue();


        private void ViewModel_ErrorOccurred(object sender, ErrorOccurredEventArgs e)
        {
            if(e.Exception is CompilationErrorException)
            {
                var ce = (CompilationErrorException)e.Exception;
                var msg = string.Join("\r\n", ce.Diagnostics.Select(x => $"{x.Location} {x.GetMessage()}"));
                DialogCoordinator.ShowMessageAsync(this, "エラー", msg );
            }
            else
            {
                DialogCoordinator.ShowMessageAsync(this, "エラー",e.Message);
            }
        }

        public MahApps.Metro.Controls.Dialogs.IDialogCoordinator DialogCoordinator
        {
            get;
            set;
        }
        public DbContextBoxViewModel DbContextBoxViewModel
        {
            get;
        }
        public TablesBoxViewModel TablesBoxViewModel
        {
            get;
        }
        public SettingsBoxViewModel SettingsBoxViewModel
        {
            get;
        }
        public ExplainBoxViewModel ExplainBoxViewModel
        {
            get;
        }
    }
}
