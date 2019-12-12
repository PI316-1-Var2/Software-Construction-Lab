using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Model.Entities
{
    public class Product
    {
        public ICollection<ProductName> Name { get; set; }

        [Key]
        public int ID { get; set; }

        [Required]
        public int Energy { get; set; }

        [Required]
        public int Proteins { get; set; }

        [Required]
        public int Fats { get; set; }

        [Required]
        public int Carbohydrates { get; set; }
    }
}
