using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpTaro.SampleDbApp
{
    [Table("orders")]
    public class Orders
    {
        [Column("cd")]
        [Key]
        public int Cd
        {
            get;set;
        }
        [Column("user_cd")]
        [Required]
        public int UserCd
        {
            get;set;
        }
        [Column("order_date")]
        public DateTime? OrderDate
        {
            get;set;
        }
    }
}
