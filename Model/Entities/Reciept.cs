using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities
{
    public class Reciept
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public Dish DishItem { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public double Price { get => Quantity * DishItem.Price; }

        [Required]
        public Visitor User { get; set; }

        [Required, Column(TypeName = "SMALLDATETIME"), Range(typeof(DateTime), "1/1/1901", "20/4/2069")]
        public DateTime Date_Order { get; set; }
    }
}
