using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpTaro.SampleAsm
{
    [Table("products")]
    public class Products
    {
        [Column("cd")]
        [Key]
        public int Cd
        {
            get; set;
        }
        [Column("name", TypeName = "character varying(32)")]
        [Required]
        public string Name
        {
            get;
            set;
        }
        [Column("unitprice")]
        public int UnitPrice
        {
            get; set;
        }
    }
}