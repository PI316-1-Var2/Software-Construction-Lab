using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Models;
using Model.Entities;

namespace Control.Controllers
{
    public class KitchenController : BaseController
    {
        public KitchenModel model;
        public KitchenController()
        {
            model = new KitchenModel();
            isActive = true;
        }
        public override string CheckInput(string body, string command)
        {
            string output = "KitchenController\n\n";
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
                Dish result = model.Add(int.Parse(parameters[0]),
                                           parameters[1],
                                           parameters[2]);
                output = "Object was successfully created:\n" + "+---+--------------+--------------+\n" +
                       string.Format(Dish.OutputPattern, "ID", "Name", "Product") +
                       "+---+--------------+--------------+\n" +
                       result.ToString();
            }
            catch (FormatException)
            {
                output = "Wrong input format.\nTry again.\n";
            }
            catch (DishException)
            {
                output = "Error occured while creating a dish.\nTry again.\n";
            }
            catch (Exception)
            {
                output = "Unexpected error occured while creating a dish.\nTry again.\n";
            }
            return output;
        }


        protected override string Change(string input)
        {
            string output = "";
            try
            {
                string[] parameters = input.Split('|');
                Dish result = model.Change(int.Parse(parameters[0]),
                                           parameters[1],
                                           parameters[2]);
                output = "Object was successfully changed:\n" + "+---+--------------+--------------+\n" +   // змінити по патерну
                       string.Format(Dish.OutputPattern, "ID", "Name", "Product") +
                       "+---+--------------+--------------+\n" +
                       result.ToString();
            }
            catch (FormatException)
            {
                output = "Wrong input format.\nTry again.\n";
            }
            catch (ProductException)
            {
                output = "Error occured while changing a dish.\nTry again.\n";
            }
            catch (Exception)
            {
                output = "Unexpected error occured while changing a dish.\nTry again.\n";
            }
            return output;
        }

        protected override string Get(string input)
        {
            string output = "";
            try
            {
                Dish result = model.Get(int.Parse(input));
                output = $"Dish#({input}):\n" + "+---+--------------+--------------+\n" +   // змінити по патерну
                       string.Format(Dish.OutputPattern, "ID", "Name", "Product") +
                       "+---+--------------+--------------+\n" +
                       result.ToString();
            }
            catch (FormatException)
            {
                output = "Wrong input format.\nTry again.\n";
            }
            catch (ProductException)
            {
                output = "Error occured while retrieving a dish.\nTry again.\n";
            }
            catch (Exception)
            {
                output = "Unexpected error occured while retrieving a dish.\nTry again.\n";
            }
            return output;
        }

        protected override string GetAll()
        {
            string output = "";
            try
            {
                List<Dish> result = model.GetAll();
                output = "Dish:\n" + "+---+--------------+--------------+\n" +
                       string.Format(Dish.OutputPattern, "ID", "Name", "Product") +
                       "+---+--------------+--------------+\n";

                foreach (var d in result)
                {
                    output += d.ToString();
                }

            }
            catch (ProductException)
            {
                output = "Error occured while retrieving dishes.\nTry again.\n";
            }
            catch (Exception)
            {
                output = "Unexpected error occured while retrieving dishes.\nTry again.\n";
            }
            return output;
        }

        protected override string Remove(string input)
        {
            string output = "";
            try
            {
                Dish result = model.Remove(int.Parse(input));
                output = $"Dish#({input}):\n" + "+---+--------------+--------------+\n" +
                       string.Format(Product.OutputPattern, "ID", "Name", "Product") +
                       "+---+--------------+--------------+\n" +
                       result.ToString();
            }
            catch (FormatException)
            {
                output = "Wrong input format.\nTry again.\n";
            }
            catch (ProductException)
            {
                output = "Error occured while removing a dish.\nTry again.\n";
            }
            catch (Exception)
            {
                output = "Unexpected error occured while removing a dish.\nTry again.\n";
            }
            return output;
        }
    }
}
