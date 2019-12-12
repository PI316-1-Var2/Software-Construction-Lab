using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control.Controllers
{
    public abstract class BaseController : IController
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
            return Globalization.UIGlobalization.ObjectGetCommands;
        }
    }
}
