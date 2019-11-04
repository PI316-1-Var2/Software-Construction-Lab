using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class WarehouseItem
    {
        public int ID;
        public string ProductName;
        public double Quantity;
        public const string OutputPattern = "|{0,-3}|{1,-14}|{2,-10}|\n";

        public WarehouseItem(int id, string name, double q)
        {
            ID = id;
            ProductName = name;
            Quantity = q;
        }

        public override string ToString()
        {
            return string.Format(OutputPattern, ID, ProductName, Quantity) + "+---+--------------+----------+\n";
        }
    }

    public class WarehouseItemException : Exception
    {
        public WarehouseItemException() : base()
        {
        }
        public WarehouseItemException(string message) : base(message)
        {
        }
        public WarehouseItemException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
