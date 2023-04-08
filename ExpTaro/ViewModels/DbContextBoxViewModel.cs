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
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ExpTaro.ViewModels
{
    public class DbContextBoxViewModel : ViewModel
    {
        public delegate void ErrorOccurredEventHandler(object sender, ErrorOccurredEventArgs e);
        public event ErrorOccurredEventHandler ErrorOccurred = delegate { };
        public delegate void MessageEventHandler(object sender, MessageEventArgs e);
        public event MessageEventHandler Message = delegate { };

        public DbContextBoxViewModel(MainWindowViewModel parent) : base()
        {
            this.Parent = parent;
            this.SourceTextDocument.TextChanged += (sender, e) =>
            {
                RaisePropertyChanged(nameof(SourceTextDocument));
            };
            this.SourceTextDocument.Text = this.Project.DatabaseContext.SourceText;
            parent.ProjectLoaded += (sender, e) =>
            {
                this.SourceTextDocument.Text = this.Project.DatabaseContext.SourceText;
            };
        }
        public MainWindowViewModel Parent
        {
            get;
        }
        public DbProject Project
        {
            get
            {
                return this.Parent.Project; 
            }
        }

        public ICSharpCode.AvalonEdit.Document.TextDocument SourceTextDocument
        {
            get;
        } = new ICSharpCode.AvalonEdit.Document.TextDocument();

        private ViewModelCommand _OpenFileCommand;

        public ViewModelCommand OpenFileCommand
        {
            get
            {
                if (_OpenFileCommand == null)
                {
                    _OpenFileCommand = new ViewModelCommand(OpenFile);
                }
                return _OpenFileCommand;
            }
        }

        public void OpenFile()
        {
            using (var dlg = new CommonOpenFileDialog())
            {
                dlg.DefaultExtension = ".cs";
                var filter = new CommonFileDialogFilter()
                {
                    DisplayName = "C# ソースファイル(*.cs)",
                };
                filter.Extensions.Add("cs");
                dlg.Filters.Add(filter);
                if(dlg.ShowDialog()== CommonFileDialogResult.Ok)
                {
                    this.Project.DatabaseContext.Path=dlg.FileName;
                    this.Project.DatabaseContext.LoadSource();
                    this.SourceTextDocument.Text = this.Project.DatabaseContext.SourceText;
                }
            }
        }

    }
}
