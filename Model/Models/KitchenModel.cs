using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entities;

namespace Model.Models
{
    public class KitchenModel
    {
        private List<Dish> dishes;
        public KitchenModel()
        {
            dishes = new List<Dish>();
        }

        public Dish Add(int id, string name, string product)
        {
            try
            {
                Dish dis = new Dish(id, name, product);
                dishes.Add(dis);
                return dis;
            }
            catch { throw new DishException(); }
        }
        public Dish Change(int id, string name, string product)
        {
            try
            {
                Dish di = dishes.Find(s => s.ID == id);
                if (di != null)
                {
                    di.ID = id;
                    di.Name = name;
                    di.Product = product;
                }
                return di;
            }
            catch
            {
                throw new DishException();
            }
        }
        public Dish Remove(int id)
        {
            try
            {
                Dish di = dishes.Find(s => s.ID == id);
                dishes.Remove(di);
                return di;
            }
            catch
            {
                throw new ProductException();
            }
        }
        public Dish Get(int id)
        {
            try
            {
                return dishes.Find(s => s.ID == id);
            }
            catch
            {
                throw new DishException();
            }
        }
        public List<Dish> GetAll()
        {
            try
            {
                return dishes;
            }
            catch
            {
                throw new DishException();
            }
        }
    }
}
