using ExpTaro.Models;
using Livet;
using Livet.Commands;
using Livet.EventListeners;
using Livet.Messaging;
using Livet.Messaging.IO;
using Livet.Messaging.Windows;
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
        public async void Initialize()
        {
            await this.Project.Settings.InitializeAsync();
        }
        public DbProject Project
        {
            get;
        }
        public MainWindowViewModel() : base()
        {
            this.Project = new DbProject();
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

        public MaterialDesignThemes.Wpf.SnackbarMessageQueue MessageQueue
        {
            get;
        } = new MaterialDesignThemes.Wpf.SnackbarMessageQueue();


        private void ViewModel_ErrorOccurred(object sender, ErrorOccurredEventArgs e)
        {
            DialogCoordinator.ShowMessageAsync(this, e.Message, "エラー");
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
