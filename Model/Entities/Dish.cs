using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class Dish
    {
        public int ID;
        public string Name;
        public string Product;
        public const string OutputPattern = "|{0,-3}|{1,-14}|{3,-14}";
        public Dish(int id, string name, string product)
        {
            ID = id;
            Name = name;
            Product = product;
        }
        public override string ToString()
        {
            return string.Format(OutputPattern, ID, Name, Product) + "+---+--------------+--------------+\n";
        }
    }

    public class DishException : Exception
    {
        public DishException() : base()
        {
        }
        public DishException(string message) : base(message)
        {
        }
        public DishException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
