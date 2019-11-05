using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class Reciept
    {
        public int ID;
        public string Name;
        public int Quantity;
        public double Price;
        public string CustomerName;
        public DateTime Date;
        public const string OutputPattern = "|{0,-3}|{1,-21}|{2,-8}|{3,-9}|{4,-15}|{5,-10}|\n";

        public Reciept(int id, string name, int quantity, double price, string customername, DateTime date)
        {
            ID = id;
            Name = name;
            Quantity = quantity;
            Price = price;
            CustomerName = customername;
            Date = date;
        }
        public override string ToString()
        {
            return string.Format(OutputPattern, ID, Name, Quantity, Price, CustomerName, Date.ToString()) + "+---+---------------------+--------+---------+---------------+----------+\n";
        }
    }

    public class RecieptException : Exception
    {
        public RecieptException() : base()
        {
        }
        public RecieptException(string message) : base(message)
        {
        }
        public RecieptException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
