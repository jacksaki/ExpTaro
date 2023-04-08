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
        }
        public MainWindowViewModel Parent
        {
            get;
        }
        public CSharpExecutor Executor
        {
            get;
            private set;
        }
    }
}
