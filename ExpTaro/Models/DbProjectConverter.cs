using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Livet;
using MessagePack;

namespace ExpTaro.Models
{
    [MessagePackObject]
    public class DbProjectConverter
    {
        public string ConvertToJson(DbProject project)
        {
            this.DatabaseContextPath = project.DatabaseContext.Path;
            this.TablePaths = project.Tables.Select(x => x.Path).ToList();
            this.GlobalAssemblies = project.Settings.GlobalAssemblies.Where(x => x.IsSelected).Select(x => x.Name).ToList();
            this.LoadedAssemblyPaths = project.Settings.LoadedAssemblies.Select(x => x.Path).ToList();
            this.Imports = project.Settings.Imports.ToList();
            return MessagePackSerializer.ConvertToJson(MessagePackSerializer.Serialize(typeof(DbProjectConverter), this));
        }

        public DbProject ConvertFromJsonAsync(string json)
        {
            
            var piyo = MessagePackSerializer.ConvertFromJson(json);
            var obj = MessagePackSerializer.Deserialize<DbProjectConverter>(piyo);
            var prj = new DbProject();
            prj.DatabaseContext.Path = obj.DatabaseContextPath;
            prj.DatabaseContext.LoadSource();
            foreach(var p in obj.TablePaths)
            {
                var table = new TableSource() { Path = p };
                table.LoadSource();
                prj.Tables.Add(table); 
            }
            foreach (var asm in prj.Settings.GlobalAssemblies)
            {
                asm.IsSelected = obj.GlobalAssemblies.Any(x => x == asm.Name);
            }
            foreach(var path in obj.LoadedAssemblyPaths)
            {
                prj.Settings.LoadedAssemblies.Add(new LoadedAssembly(path));
            }
            foreach(var imp in obj.Imports)
            {
                prj.Settings.Imports.Add(imp);
            }
            return prj;
        }

        [Key(0)]
        public string DatabaseContextPath
        {
            get;
            set;
        }
        [Key(1)]
        public List<string> TablePaths
        {
            get;
            set;
        } = new List<string>();

        [Key(2)]
        public List<string> GlobalAssemblies
        {
            get;
            set;
        } = new List<string>();

        [Key(3)]
        public List<string> LoadedAssemblyPaths
        {
            get;
            set;
        } = new List<string>();

        [Key(4)]
        public List<string> Imports
        {
            get;
            set;
        } = new List<string>();
    }
}
