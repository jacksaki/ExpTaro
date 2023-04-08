using Livet;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpTaro.Models
{
    public class DbProject:NotificationObject
    {
        public DatabaseContext DatabaseContext
        {
            get;
        } = new DatabaseContext();
        public ObservableCollection<TableSource> Tables
        {
            get;
        } = new ObservableCollection<TableSource>();

        public DbProjectSettings Settings
        {
            get;
        } = new DbProjectSettings();

        private string _QuerySource;

        public string QuerySource
        {
            get
            {
                return _QuerySource;
            }
            set
            { 
                if (_QuerySource == value)
                {
                    return;
                }
                _QuerySource = value;
                RaisePropertyChanged();
            }
        }

    }
}
