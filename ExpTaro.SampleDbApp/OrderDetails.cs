using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpTaro.SampleDbApp
{
    [Table("order_details")]
    public class OrderDetails
    {
        [Column("order_cd")]
        [Key]
        public int OrderCd
        {
            get;set;
        }
        [Column("order_detail_cd")]
        [Key]
        public int OrderDetailCd
        {
            get;set;
        }
       [Column("product_cd")]
        [Required]
        public int ProductCd
        {
            get;set;
        }
       [Column("unitprice")]
        [Required]
        public int Unitprice
        {
            get;set;
        }
       [Column("order_count")]
        [Required]
       public int OrderCount
        {
            get;set;
        }
    }
}
