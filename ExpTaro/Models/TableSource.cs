using Livet;
using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ExpTaro.Models
{
    public class TableSource:NotificationObject
    {
        public string FileName
        {
            get
            {
                return System.IO.Path.GetFileName(this.Path);
            }
        }

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
                RaisePropertyChanged(FileName);
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

        public void LoadSource()
        {
            if (System.IO.File.Exists(this.Path))
            {
                this.SourceText = System.IO.File.ReadAllText(this.Path);
            }
        }
    }
}
