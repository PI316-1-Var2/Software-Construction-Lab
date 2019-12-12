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
        private readonly UnitOfWork unitOfWork;
        public RecieptModel(string connection)
        {
            unitOfWork = new UnitOfWork(connection);
        }

        public Reciept Add(int dishId, int quantity, int customerId, DateTime date_order)
        {
            Dish dish = unitOfWork.Dishes.Get(dishId) ?? throw new ArgumentNullException("Incorrect dish Id!");
            Visitor vis = unitOfWork.Visitors.Get(customerId) ?? throw new ArgumentNullException("Incorrect customer Id!");
            Reciept r = new Reciept
            {
                User = vis,
                DishItem = dish,
                Date_Order = date_order,
                Quantity = quantity
            };
            unitOfWork.Reciepts.Add(r);
            unitOfWork.Complete();
            return r;
        }

        public Reciept Change(int id, int dishId, int quantity, int customerId, DateTime date_order)
        {
            Reciept r = unitOfWork.Reciepts.Get(id) ?? throw new ArgumentNullException("Incoorect ID!");
            Dish dish = unitOfWork.Dishes.Get(dishId) ?? throw new ArgumentNullException("Incorrect dish ID!");
            Visitor vis = unitOfWork.Visitors.Get(customerId) ?? throw new ArgumentNullException("Incorrect visitor ID!");
            r.DishItem = dish;
            r.Quantity = quantity;
            r.User = vis;
            r.Date_Order = date_order;
            unitOfWork.Complete();
            return r;
        }

        public Reciept Remove(int id)
        {
            Reciept ch = unitOfWork.Reciepts.Get(id) ?? throw new ArgumentOutOfRangeException("Incorrect ID!");
            unitOfWork.Reciepts.Remove(id);
            unitOfWork.Complete();
            return ch;
        }
        public Reciept Get(int id)
        {
            var r = unitOfWork.Reciepts.Get(id) ?? throw new ArgumentOutOfRangeException("Incorrect ID!");
            var dish = unitOfWork.Dishes.Get(r.DishItem.ID);
            r.DishItem = dish;
            return r;
        }

        public List<Reciept> GetAll()
        {
            var list = unitOfWork.Reciepts.GetAll().ToList();
            foreach (var r in list)
            {
                var dish = unitOfWork.Dishes.Get(r.DishItem.ID);
                r.DishItem = dish;
            }
            return list;
        }

        public List<Reciept> GetbyDate(int days)
        {
            TimeSpan time = new TimeSpan(days, 0, 0, 0);
            var list = GetAll().Where(r => DateTime.Now - r.Date_Order <= time).ToList();
            foreach (var r in list)
            {
                var dish = unitOfWork.Dishes.Get(r.DishItem.ID);
                r.DishItem = dish;
            }
            return list;
        }

        public List<Reciept> CreateReciept(int customerId, DateTime order_datetime)
        {
            var list = GetAll().Where(r => r.User.ID == customerId && r.Date_Order == order_datetime).ToList();
            foreach (var r in list)
            {
                var dish = unitOfWork.Dishes.Get(r.DishItem.ID);
                r.DishItem = dish;
            }
            return list;
        }
    }
}
