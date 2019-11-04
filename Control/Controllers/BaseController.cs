using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control.Controllers
{
    public abstract class BaseController
    {
        public bool isActive { get; protected set; }
        protected abstract string Add(string input);
        protected abstract string Remove(string input);
        protected abstract string Change(string input);
        public abstract string CheckInput(string input, string command);
        protected abstract string Get(string input);
        protected abstract string GetAll();
        public virtual string GetCommands()
        {
            return "\n1. Get item by Id;" +
                   "\n2. Get all items;" +
                   "\n3. Add new item;" +
                   "\n4. Remove item by Id;" +
                   "\n5. Edit item;\n" +
                   "\n0. Return to home screen.\n";
        }
    }
}
