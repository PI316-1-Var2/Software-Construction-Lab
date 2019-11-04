using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control.Controllers
{
    public class CashierController : BaseController
    {
        public CashierModel model;
        public CashierController()
        {
            model = new CashierModel();
            isActive = true;
        }

        public override string CheckInput(string body, string command)
        {
            string output = "CashierController\n\n";
            switch (command.ToLower())
            {
                case "1":
                    output += Get(body);
                    break;
                case "2":
                    output += GetAll();
                    break;
                case "3":
                    output += Add(body);
                    break;
                case "4":
                    output += Remove(body);
                    break;
                case "5":
                    output += Change(body);
                    break;
                case "0":
                    isActive = false;
                    break;
                case null:
                    break;
                default:
                    output += "Unsupported command!";
                    break;
            }
            return output + GetCommands();
        }
        protected override string Add(string input)
        {
            string output = "";
            try
            {
                string[] parameters = input.Split('|');
                Basket result = model.Add(int.Parse(parameters[0]),
                                           parameters[1]);
                output = "Object was successfully created:\n" + "+---+--------------------------------+\n" +
                       string.Format(Basket.OutputPattern, "ID", "Dishes") +
                       "+---+--------------------------------+\n" +
                       result.ToString();
            }
            catch (FormatException)
            {
                output = "Wrong input format.\nTry again.\n";
            }
            catch (BasketException)
            {
                output = "Error occured while creating a Basket.\nTry again.\n";
            }
            catch (Exception)
            {
                output = "Unexpected error occured while creating a Basket.\nTry again.\n";
            }
            return output;
        }


        protected override string Change(string input)
        {
            string output = "";
            try
            {
                string[] parameters = input.Split('|');
                Basket result = model.Change(int.Parse(parameters[0]),
                                           parameters[1]);
                output = "Object was successfully changed:\n" + "+---+--------------------------------+\n" +   // змінити по патерну
                       string.Format(Basket.OutputPattern, "ID", "Dishes") +
                       "+---+--------------------------------+\n" +
                       result.ToString();
            }
            catch (FormatException)
            {
                output = "Wrong input format.\nTry again.\n";
            }
            catch (BasketException)
            {
                output = "Error occured while changing a Basket.\nTry again.\n";
            }
            catch (Exception)
            {
                output = "Unexpected error occured while changing a Basket.\nTry again.\n";
            }
            return output;
        }

        protected override string Get(string input)
        {
            string output = "";
            try
            {
                Basket result = model.Get(int.Parse(input));
                output = $"Basket#({input}):\n" + "+---+--------------------------------+\n" +   // змінити по патерну
                       string.Format(Basket.OutputPattern, "ID", "Dishes") +
                       "+---+--------------------------------+\n" +
                       result.ToString();
            }
            catch (FormatException)
            {
                output = "Wrong input format.\nTry again.\n";
            }
            catch (BasketException)
            {
                output = "Error occured while retrieving a Basket.\nTry again.\n";
            }
            catch (Exception)
            {
                output = "Unexpected error occured while retrieving a Basket.\nTry again.\n";
            }
            return output;
        }

        protected override string GetAll()
        {
            string output = "";
            try
            {
                List<Basket> result = model.GetAll();
                output = "Basket:\n" + "+---+--------------------------------+\n" +
                       string.Format(Basket.OutputPattern, "ID", "Dishes") +
                       "+---+--------------------------------+\n";

                foreach (var d in result)
                {
                    output += d.ToString();
                }

            }
            catch (BasketException)
            {
                output = "Error occured while retrieving Baskets.\nTry again.\n";
            }
            catch (Exception)
            {
                output = "Unexpected error occured while retrieving Baskets.\nTry again.\n";
            }
            return output;
        }

        protected override string Remove(string input)
        {
            string output = "";
            try
            {
                Basket result = model.Remove(int.Parse(input));
                output = $"Basket#({input}):\n" + "+---+--------------------------------+\n" +
                       string.Format(Product.OutputPattern, "ID", "Dishes") +
                       "+---+--------------------------------+\n" +
                       result.ToString();
            }
            catch (FormatException)
            {
                output = "Wrong input format.\nTry again.\n";
            }
            catch (BasketException)
            {
                output = "Error occured while removing a Basket.\nTry again.\n";
            }
            catch (Exception)
            {
                output = "Unexpected error occured while removing a Basket.\nTry again.\n";
            }
            return output;
        }
    }
}
