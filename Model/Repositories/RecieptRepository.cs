using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entities;
using Model.Interfaces;

namespace Model.Repositories
{
    public class RecieptRepository : Repository<Reciept>, IRecieptRepository
    {
        private new CanteenContext Context => base.Context as CanteenContext;

        public RecieptRepository(CanteenContext context) : base(context)
        {

        }

        Reciept IRecieptRepository.Get(int id)
        {
            return Context.Reciepts.Include("DishItem").Include("CustomerItem").First(e => e.ID == id);
        }

        IEnumerable<Reciept> IRecieptRepository.GetAll()
        {
            return Context.Reciepts.Include("DishItem").Include("CustomerItem").ToList();
        }
    }
}
