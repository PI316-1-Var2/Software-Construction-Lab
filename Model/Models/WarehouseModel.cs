using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entities;

namespace Model.Models
{
    public class WarehouseModel
    {
        private List<WarehouseItem> items;
        public WarehouseModel()
        {
            items = new List<WarehouseItem>();
        }

        public WarehouseItem Add(int ID, string name, int q)
        {
            try
            {
                WarehouseItem wi = new WarehouseItem(ID, name, q);
                items.Add(wi);
                return wi;
            }
            catch { throw new WarehouseItemException(); }
        }
        public WarehouseItem Change(int ID, string name, int q)
        {
            try
            {
                WarehouseItem wi = items.Find(w => w.ID == ID);
                if (wi != null)
                {
                    wi.ID = ID;
                    wi.ProductName = name;
                    wi.Quantity = q;
                }
                return wi;
            }
            catch
            {
                throw new WarehouseItemException();
            }
        }
        public WarehouseItem Remove(int ID)
        {
            try
            {
                WarehouseItem wi = items.Find(w => w.ID == ID);
                items.Remove(wi);
                return wi;
            }
            catch
            {
                throw new WarehouseItemException();
            }
        }
        public WarehouseItem Get(int ID)
        {
            try
            {
                return items.Find(w => w.ID == ID);
            }
            catch
            {
                throw new WarehouseItemException();
            }
        }
        public List<WarehouseItem> GetAll()
        {
            try
            {
                return items;
            }
            catch
            {
                throw new WarehouseItemException();
            }
        }
    }
}
