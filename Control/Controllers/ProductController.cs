using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Models;
using Model.Entities;

namespace Control.Controllers
{
    public class ProductController : BaseController
    {
        public ProductModel model;
        public ProductController()
        {
            model = new ProductModel();
            isActive = true;
        }
        public override string CheckInput(string body, string command)
        {
            string output = "ProductController\n\n";
            switch (command.ToLower())
            {
                case "1.":
                    output += Get(body);
                    break;
                case "2.":
                    output += GetAll();
                    break;
                case "3.":
                    output += Add(body);
                    break;
                case "4.":
                    output += Remove(body);
                    break;
                case "5.":
                    output += Change(body);
                    break;
                case "0.":
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
                Product result = model.Add(int.Parse(parameters[0]),
                                           parameters[1],
                                           int.Parse(parameters[2]),
                                           int.Parse(parameters[3]),
                                           int.Parse(parameters[4]),
                                           int.Parse(parameters[5]));
                output = "Object was successfully created:\n" + "+---+--------------+------+-------+---+-------------+\n" +
                       string.Format(Product.OutputPattern, "ID", "Name", "Energy", "Protein", "Fat", "Carbohydrates") +
                       "+---+--------------+------+-------+---+-------------+\n" +
                       result.ToString();
            }
            catch (FormatException)
            {
                output = "Wrong input format.\nTry again.\n";
            }
            catch (ProductException)
            {
                output = "Error occured while creating a product.\nTry again.\n";
            }
            catch (Exception)
            {
                output = "Unexpected error occured while creating a product.\nTry again.\n";
            }
            return output;
        }
        //рівень помилок : Error, Fatal, Info, Debug

        protected override string Change(string input)
        {
            string output = "";
            try
            {
                string[] parameters = input.Split('|');
                Product result = model.Change(int.Parse(parameters[0]),
                                           parameters[1],
                                           int.Parse(parameters[2]),
                                           int.Parse(parameters[3]),
                                           int.Parse(parameters[4]),
                                           int.Parse(parameters[5]));
                output = "Object was successfully changed:\n" + "+---+--------------+---+---+---+---+\n" +   // змінити по патерну
                       string.Format(Product.OutputPattern, "ID", "Name", "Energy", "Protein", "Fat", "Carbohydrates") +
                       "+---+--------------+---+---+---+---+---+\n" +
                       result.ToString();
            }
            catch (FormatException)
            {
                output = "Wrong input format.\nTry again.\n";
            }
            catch (ProductException)
            {
                output = "Error occured while editing a product.\nTry again.\n";
            }
            catch (Exception)
            {
                output = "Unexpected error occured while editing a product.\nTry again.\n";
            }
            return output;
        }

        protected override string Get(string input)
        {
            string output = "";
            try
            {
                Product result = model.Get(int.Parse(input));
                output = $"Product#({input}):\n" + "+---+--------------+---+---+---+---+\n" +   // змінити по патерну
                       string.Format(Product.OutputPattern, "ID", "Name", "Energy", "Protein", "Fat", "Carbohydrates") +
                       "+---+--------------+---+---+---+---+---+\n" +
                       result.ToString();
            }
            catch (FormatException)
            {
                output = "Wrong input format.\nTry again.\n";
            }
            catch (ProductException)
            {
                output = "Error occured while retrieving a product.\nTry again.\n";
            }
            catch (Exception)
            {
                output = "Unexpected error occured while retrieving a product.\nTry again.\n";
            }
            return output;
        }

        protected override string GetAll()
        {
            string output = "";
            try
            {
                List<Product> result = model.GetAll();
                output = "Products:\n" + "+---+--------------+---+---+---+---+\n" +
                       string.Format(Product.OutputPattern, "ID", "Name", "Energy", "Protein", "Fat", "Carbohydrates") +
                       "+---+--------------+---+---+---+---+---+\n";

                foreach (var p in result)
                {
                    output += p.ToString();
                }

            }
            catch (ProductException)
            {
                output = "Error occured while retreiving products.\nTry again.\n";
            }
            catch (Exception)
            {
                output = "Unexpected error occured while retrieving products.\nTry again.\n";
            }
            return output;
        }

        protected override string Remove(string input)
        {
            string output = "";
            try
            {
                Product result = model.Remove(int.Parse(input));
                output = $"Product#({input}):\n" + "+---+--------------+---+---+---+---+\n" +
                       string.Format(Product.OutputPattern, "ID", "Name", "Energy", "Protein", "Fat", "Carbohydrates") +
                       "+---+--------------+---+---+---+---+---+\n" +
                       result.ToString();
            }
            catch (FormatException)
            {
                output = "Wrong input format.\nTry again.\n";
            }
            catch (ProductException)
            {
                output = "Error occured while removing a product.\nTry again.\n";
            }
            catch (Exception)
            {
                output = "Unexpected error occured while removing a product.\nTry again.\n";
            }
            return output;
        }
    }
}
