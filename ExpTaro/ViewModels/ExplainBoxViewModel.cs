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

namespace ExpTaro.ViewModels
{
    public class ExplainBoxViewModel : ViewModel
    {
        public delegate void ErrorOccurredEventHandler(object sender, ErrorOccurredEventArgs e);
        public event ErrorOccurredEventHandler ErrorOccurred = delegate { };
        public delegate void MessageEventHandler(object sender, MessageEventArgs e);
        public event MessageEventHandler Message = delegate { };
        public ExplainBoxViewModel(MainWindowViewModel parent) : base()
        {
            this.Parent = parent;
            this.SourceTextDocument.TextChanged += (sender, e) =>
            {
                this.Project.QuerySource = this.SourceTextDocument.Text;
                ExecuteCommand.RaiseCanExecuteChanged();
                RaisePropertyChanged(nameof(SourceTextDocument));
            }; 
        }
        public MainWindowViewModel Parent
        {
            get;
        }

        public ICSharpCode.AvalonEdit.Document.TextDocument SourceTextDocument
        {
            get;
        } = new ICSharpCode.AvalonEdit.Document.TextDocument();
        public CSharpExecutor Executor
        {
            get;
        } = new CSharpExecutor();


        private ViewModelCommand _ExecuteCommand;

        public ViewModelCommand ExecuteCommand
        {
            get
            {
                if (_ExecuteCommand == null)
                {
                    _ExecuteCommand = new ViewModelCommand(Execute, CanExecute);
                }
                return _ExecuteCommand;
            }
        }
        public DbProject Project
        {
            get
            {
                return this.Parent.Project;
            }
        }
        public bool CanExecute()
        {
            try
            {
                this.Project.Validate();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async void Execute()
        {
            try
            {
                await this.Executor.ExecuteAsync(this.Project);
            }
            catch (Exception ex)
            {
                this.ErrorOccurred(this, new ErrorOccurredEventArgs(ex.Message, ex));
            }
        }

    }
}
