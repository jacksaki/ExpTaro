using Livet;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;

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

            var scriptOptions = ScriptOptions.Default.
                AddReferences(project.Settings.GlobalAssemblies.Where(x => x.IsSelected).Select(x => x.Assembly)).
                AddReferences(project.Settings.LoadedAssemblies.Select(x => x.Assembly)).
                AddImports(project.Settings.Imports);
            var sb = new System.Text.StringBuilder();
            if(!string.IsNullOrWhiteSpace(project.DatabaseContext.SourceText))
            {
                sb.AppendLine(project.DatabaseContext.SourceText);
            }
            foreach(var table in project.Tables)
            {
                if(!string.IsNullOrWhiteSpace(table.SourceText))
                {
                    sb.AppendLine(table.SourceText);
                }
            }
            var script = CSharpScript.Create(sb.ToString(), scriptOptions);
            script = script.ContinueWith(project.QuerySource);
            var compilation = script.GetCompilation();
            var errors = compilation.GetDiagnostics().Where(d => d.Severity == DiagnosticSeverity.Error);
            if (errors.Any())
            {
                foreach (var error in errors)
                {
                    Logs.Add(new LogItem($"{error.Location} {error.GetMessage()}", LogType.Error));
                }
                return;
            }


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
                try
                {
                    var state = await script.RunAsync();
                }
                catch (CompilationErrorException ce)
                {
                    foreach(var msg in ce.Diagnostics)
                    {
                        Logs.Add(new LogItem($"{msg.Location} {msg.GetMessage()}", LogType.Error));
                    }
                }
                catch (Exception ex)
                {
                    Logs.Add(new LogItem(ex.Message, LogType.Error));
                    Logs.Add(new LogItem(ex.StackTrace, LogType.Error));
                }
            }
        }
        public DispatcherCollection<LogItem> Logs
        {
            get;
        } = new DispatcherCollection<LogItem>(DispatcherHelper.UIDispatcher);
    }
}
