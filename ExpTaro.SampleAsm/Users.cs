using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpTaro.SampleAsm
{
    [Table("users")]
    public class Users
    {
        [Key]
        public int Cd
        {
            get;set;
        }
        [Column("name",TypeName = "character varying(32)")]
        [Required]
        public string Name
        {
            get;
            set;
        }
    }
}
