using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entities;

namespace Model.Models
{
    public class ProductModel
    {
        private List<Product> products;
        public ProductModel()
        {
            products = new List<Product>();
        }

        public Product Add(int id, string name, int energy, int proteins, int fats, int carbohydrates) // throw exception
        {
            try
            {
                Product prod = new Product(id, name, energy, proteins, fats, carbohydrates);
                products.Add(prod);
                return prod;
            }
            catch { throw new ProductException(); }
        }
        public Product Change(int id, string name, int energy, int proteins, int fats, int carbohydrates)
        {
            try
            {
                Product pr = products.Find(s => s.ID == id);
                if (pr != null)
                {
                    pr.ID = id;
                    pr.Name = name;
                    pr.Energy = energy;
                    pr.Proteins = proteins;
                    pr.Fats = fats;
                    pr.Carbohydrates = carbohydrates;
                }
                return pr;
            }
            catch
            {
                throw new ProductException();
            }
        }
        public Product Remove(int id)
        {
            try
            {
                Product pr = products.Find(s => s.ID == id);
                products.Remove(pr);
                return pr;
            }
            catch
            {
                throw new ProductException();
            }
        }
        public Product Get(int id)
        {
            try
            {
                return products.Find(s => s.ID == id);
            }
            catch
            {
                throw new ProductException();
            }
        }
        public List<Product> GetAll()
        {
            try
            {
                return products;
            }
            catch
            {
                throw new ProductException();
            }
        }
    }
}
