using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entities;
using Model.Interfaces;
using Model.Repositories;

namespace Model
{
    public class UnitOfWork
    {
        private readonly CanteenContext _context;

        private IRecieptRepository recieptRepository;
        public IRecieptRepository Reciepts => recieptRepository ?? (recieptRepository = new RecieptRepository(_context));


        private IDishRepository dishRepository;
        public IDishRepository Dishes => dishRepository ?? (dishRepository = new DishRepository(_context));


        private IProductRepository productRepository;
        public IProductRepository Products => productRepository ?? (productRepository = new ProductRepository(_context));

        private IWarehouseRepository whRepository;
        public IWarehouseRepository WarehouseItems => whRepository ?? (whRepository = new WarehouseRepository(_context));

        private IRepository<Visitor> visitorRepository;
        public IRepository<Visitor> Visitors => visitorRepository ?? (visitorRepository = new Repository<Visitor>(_context));

        public UnitOfWork(string connection)
        {
            _context = new CanteenContext(connection);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
