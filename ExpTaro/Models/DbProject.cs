using Livet;
using MessagePack;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ExpTaro.Models
{
    [MessagePackObject(false)]
    public class DbProject:NotificationObject
    {
        public event EventHandler Loaded = delegate { };
        public static string JsonPath
        {
            get
            {
                return System.IO.Path.ChangeExtension(System.Reflection.Assembly.GetExecutingAssembly().Location, ".json");
            }
        }
        public static DbProject Load()
        {
            if (System.IO.File.Exists(JsonPath))
            {
                return new DbProjectConverter().ConvertFromJsonAsync(System.IO.File.ReadAllText(JsonPath));
            }
            else
            {
                return new DbProject();
            }
        }
        public void Save()
        {
            var json = new DbProjectConverter().ConvertToJson(this);
            System.IO.File.WriteAllText(JsonPath, json);
        }

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
