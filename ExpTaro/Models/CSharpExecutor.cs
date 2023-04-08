using Livet;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExpTaro.Models
{
    public class CSharpExecutor:NotificationObject
    {
        public CSharpExecutor()
        {
            this.Logs.CollectionChanged += (sender, e) =>
            {
                RaisePropertyChanged(nameof(Logs)); 
            };
        }
        public async Task ExecuteAsync(DbProject project)
        {
            this.Logs.Clear();
            var option = ScriptOptions.Default.AddReferences(project.Settings.Assemblies).AddImports(project.Settings.Imports);
            var script = CSharpScript.Create(project.QuerySource, options: option);
            using (var rd = new OutputRedirector())
            {
                rd.StandardOutputWritten += (sender, e) =>
                {
                    Logs.Add(new LogItem(e.Text, LogType.Info));
                };
                rd.StandardErrorWritten += (sender, e) =>
                {
                    Logs.Add(new LogItem(e.Text, LogType.Error));
                };
                await script.RunAsync();
            }
        }
        public DispatcherCollection<LogItem> Logs
        {
            get;
        } = new DispatcherCollection<LogItem>(DispatcherHelper.UIDispatcher);
    }
}
