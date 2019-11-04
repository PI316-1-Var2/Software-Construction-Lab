using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class Product
    {
        public int ID;
        public string Name;
        public int Energy;
        public int Proteins;
        public int Fats;
        public int Carbohydrates;
        public const string OutputPattern = "|{0,-3}|{1,-14}|{2,-6}|{3,-8}|{4,-4}|{5,-13}|\n"; // - left, + right
        public Product(int id, string name, int energy, int proteins, int fats, int carbohydrates)
        {
            ID = id;
            Name = name;
            Energy = energy;
            Proteins = proteins;
            Fats = fats;
            Carbohydrates = carbohydrates;
        }
        public override string ToString()
        {
            return string.Format(OutputPattern, ID, Name, Energy, Proteins, Fats, Carbohydrates) + "+---+--------------+------+-------+---+-------------+\n";
        }
    }

    public class ProductException : Exception
    {
        public ProductException() : base()
        {
        }
        public ProductException(string message) : base(message)
        {
        }
        public ProductException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
