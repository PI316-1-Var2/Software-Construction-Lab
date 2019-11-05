using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entities;

namespace Model.Models
{
    public class RecieptModel
    {
        private List<Reciept> reciepts;
        public RecieptModel()
        {
            reciepts = new List<Reciept>();
        }
        public Reciept Add(int ID, string name, int quantity, double price, string name_cus, DateTime date_order)
        {
            try
            {
                Reciept ci = new Reciept(ID, name, quantity, price, name_cus, date_order);
                reciepts.Add(ci);
                return ci;
            }
            catch
            {
                throw new RecieptException();
            }
        }
        public Reciept Change(int ID, string name, int quantity, double price, string customername, DateTime date)
        {
            try
            {
                Reciept ci = reciepts.Find(s => s.ID == ID);
                if (ci != null)
                {
                    ci.Name = name;
                    ci.Quantity = quantity;
                    ci.Price = price;
                    ci.CustomerName = customername;
                    ci.Date = date;
                }
                return ci;
            }
            catch
            {
                throw new RecieptException();
            }
        }
        public Reciept Remove(int ID)
        {
            try
            {
                Reciept ch = reciepts.Find(s => s.ID == ID);
                reciepts.Remove(ch);
                return ch;
            }
            catch
            {
                throw new RecieptException();
            }

        }
        public Reciept Get(int ID)
        {
            try
            {
                return reciepts.Find(s => s.ID == ID);
            }
            catch
            {
                throw new RecieptException();
            }
        }
        public List<Reciept> GetAll()
        {
            try
            {
                return reciepts;
            }
            catch
            {
                throw new RecieptException();
            }
        }

        public List<Reciept> GetbyDate(int days)
        {
            try
            {
                List<Reciept> items = new List<Reciept>();
                TimeSpan time = new TimeSpan(days, 0, 0, 0);
                foreach (var c in reciepts)
                {
                    if (DateTime.Now - c.Date <= time)
                    {
                        items.Add(c);
                    }
                }
                return items;

            }
            catch
            {
                throw new RecieptException();
            }
        }
    }
}
