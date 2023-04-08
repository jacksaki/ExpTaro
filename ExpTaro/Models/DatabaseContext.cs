using Livet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
namespace ExpTaro.Models
{
    public class DatabaseContext:NotificationObject
    {

        private string _Path;

        public string Path
        {
            get
            {
                return _Path;
            }
            set
            { 
                if (_Path == value)
                {
                    return;
                }
                _Path = value;
                RaisePropertyChanged();
            }
        }

        private string _SourceText;

        public string SourceText
        {
            get
            {
                return _SourceText;
            }
            set
            { 
                if (_SourceText == value)
                {
                    return;
                }
                _SourceText = value;
                RaisePropertyChanged();
            }
        }

        public async Task LoadSourceAsync()
        {
            this.SourceText = await System.IO.File.ReadAllTextAsync(this.Path); 
        }
    }
}
