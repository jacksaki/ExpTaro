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
    }
}
