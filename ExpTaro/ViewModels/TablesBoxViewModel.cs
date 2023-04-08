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
using System.Text;

namespace ExpTaro.ViewModels
{
    public class TablesBoxViewModel : ViewModel
    {
        public delegate void ErrorOccurredEventHandler(object sender, ErrorOccurredEventArgs e);
        public event ErrorOccurredEventHandler ErrorOccurred = delegate { };
        public delegate void MessageEventHandler(object sender, MessageEventArgs e);
        public event MessageEventHandler Message = delegate { };
        public TablesBoxViewModel(MainWindowViewModel parent) : base()
        {
            this.Parent = parent;
            SelectedTableSourceDocument.TextChanged += (sender, e) =>
            {
                RaisePropertyChanged(nameof(SelectedTableSourceDocument));
            };
        }
        public MainWindowViewModel Parent
        {
            get;
        }

        public ICSharpCode.AvalonEdit.Document.TextDocument SelectedTableSourceDocument
        {
            get;
        } = new ICSharpCode.AvalonEdit.Document.TextDocument();

        private TableSource _SelectedTable;

        public TableSource SelectedTable
        {
            get
            {
                return _SelectedTable;
            }
            set
            { 
                if (_SelectedTable == value)
                {
                    return;
                }
                _SelectedTable = value;
                if (value != null)
                {
                    this.SelectedTableSourceDocument.Text = value.SourceText;
                }
                else
                {
                    this.SelectedTableSourceDocument.Text = "";
                }
                RemoveSelectedCommand.RaiseCanExecuteChanged();
                RaisePropertyChanged();
            }
        }


        private ViewModelCommand _RemoveSelectedCommand;

        public ViewModelCommand RemoveSelectedCommand
        {
            get
            {
                if (_RemoveSelectedCommand == null)
                {
                    _RemoveSelectedCommand = new ViewModelCommand(RemoveSelected, CanRemoveSelected);
                }
                return _RemoveSelectedCommand;
            }
        }

        public bool CanRemoveSelected()
        {
            return this.SelectedTable != null;
        }

        public void RemoveSelected()
        {
            this.Project.Tables.Remove(this.SelectedTable);
        }


        public DbProject Project
        {
            get
            {
                return this.Parent.Project;
            }
        }

        private ViewModelCommand _AddTableCommand;

        public ViewModelCommand AddTableCommand
        {
            get
            {
                if (_AddTableCommand == null)
                {
                    _AddTableCommand = new ViewModelCommand(AddTable);
                }
                return _AddTableCommand;
            }
        }

        public void AddTable()
        {
            using (var dlg = new CommonOpenFileDialog())
            {
                dlg.Multiselect = true;
                dlg.DefaultExtension = ".cs";
                var filter = new CommonFileDialogFilter()
                {
                    DisplayName = "C# ソースファイル(*.cs)",
                };
                filter.Extensions.Add("cs");
                dlg.Filters.Add(filter);
                if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    foreach(var file in dlg.FileNames)
                    {
                        var table = new TableSource();
                        table.Path = file;
                        table.LoadSource();
                        this.Project.Tables.Add(table);
                        this.SelectedTable = table;
                    }
                }
            }
        }

    }
}
