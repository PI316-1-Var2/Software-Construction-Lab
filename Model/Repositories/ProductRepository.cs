using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Model.Entities;
using Model.Interfaces;

namespace Model.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private new CanteenContext Context => base.Context as CanteenContext;

        public ProductRepository(CanteenContext context) : base(context)
        {

        }
        Product IProductRepository.Get(int id)
        {
            var res = Context.Products.Include("Name").Where(p => p.ID == id).First();
            if (res.Name.Count > 1)
            {
                var list = res.Name.ToList();
                var name = list.Find(n => n.Language == Thread.CurrentThread.CurrentCulture.Name);
                list.Remove(name);
                list.Insert(0, name);
                res.Name = list;
            }
            return res;
        }

        IEnumerable<Product> IProductRepository.GetAll()
        {
            var res = Context.Products.Include("Name");
            foreach (var p in res)
            {
                if (p.Name.Count > 1)
                {
                    var list = p.Name.ToList();
                    var name = list.Find(n => n.Language == Thread.CurrentThread.CurrentCulture.Name);
                    list.Remove(name);
                    list.Insert(0, name);
                    p.Name = list;
                }
            }
            return res;
        }
    }
}
