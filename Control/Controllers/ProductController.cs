using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Models;
using Model.Entities;
using Logging;

namespace Control.Controllers
{
    public class ProductController : BaseController
    {
        public ProductModel model;
        private ILogger logger = LabLogger.GetLoggingService();
        public ProductController()
        {
            model = new ProductModel();
            isActive = true;
        }
        public override string CheckInput(string body, string command)
        {
            logger.Debug($"Received user input[Command:{command}, Body:{body}].");
            string output = "ProductController\n\n(Body format: Id|Name|Energy|Proteins|Fats|Carbohydrates)\n\n";
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
                    logger.Debug("User called for non-existant function.");
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
                logger.Info($"Attempt to add Product({input})...");
                string[] parameters = input.Split('|');
                Product result = model.Add(int.Parse(parameters[0]),
                                           parameters[1],
                                           int.Parse(parameters[2]),
                                           int.Parse(parameters[3]),
                                           int.Parse(parameters[4]),
                                           int.Parse(parameters[5]));
                logger.Info($"Product({input}) was created successfully.");
                output = "Object was successfully created:\n" + "+---+--------------+------+--------+----+-------------+\n" +
                       string.Format(Product.OutputPattern, "ID", "Name", "Energy", "Proteins", "Fats", "Carbohydrates") +
                       "+---+--------------+------+--------+----+-------------+\n" +
                       result.ToString();
            }
            catch (FormatException fe)
            {
                logger.Error(fe, $"Cannot covert values.");
                output = "Wrong input format.\nTry again.\n";
            }
            catch (ProductException pe)
            {
                logger.Error(pe, $"Cannot create Product({input}).");
                output = "Error occured while creating a product.\nTry again.\n";
            }
            catch (Exception e)
            {
                logger.Fatal(e, $"Unexpected error.");
                output = "Unexpected error occured while creating a product.\nTry again.\n";
            }
            return output;
        }

        protected override string Change(string input)
        {
            string output = "";
            try
            {
                logger.Info($"Attempt to edit Product({input})...");
                string[] parameters = input.Split('|');
                Product result = model.Change(int.Parse(parameters[0]),
                                           parameters[1],
                                           int.Parse(parameters[2]),
                                           int.Parse(parameters[3]),
                                           int.Parse(parameters[4]),
                                           int.Parse(parameters[5]));
                logger.Info($"Product({input}) was updated successfully.");
                output = "Object was successfully changed:\n" + "+---+--------------+------+--------+----+-------------+\n" +
                       string.Format(Product.OutputPattern, "ID", "Name", "Energy", "Protein", "Fat", "Carbohydrates") +
                       "+---+--------------+------+--------+----+-------------+\n" +
                       result.ToString();
            }
            catch (FormatException fe)
            {
                logger.Error(fe, $"Cannot covert values.");
                output = "Wrong input format.\nTry again.\n";
            }
            catch (ProductException pe)
            {
                logger.Error(pe, $"Cannot edit Product({input}).");
                output = "Error occured while editing a product.\nTry again.\n";
            }
            catch (Exception e)
            {
                logger.Fatal(e, $"Unexpected error.");
                output = "Unexpected error occured while editing a product.\nTry again.\n";
            }
            return output;
        }

        protected override string Get(string input)
        {
            string output = "";
            try
            {
                logger.Info($"Attempt to retrieve Product({input})...");
                Product result = model.Get(int.Parse(input));
                logger.Info($"Product({input}) was retrieved successfully.");
                output = $"Product#({input}):\n" + "+---+--------------+------+--------+----+-------------+\n" +
                       string.Format(Product.OutputPattern, "ID", "Name", "Energy", "Protein", "Fat", "Carbohydrates") +
                       "+---+--------------+------+--------+----+-------------+\n" +
                       result.ToString();
            }
            catch (FormatException fe)
            {
                logger.Error(fe, $"Cannot covert values.");
                output = "Wrong input format.\nTry again.\n";
            }
            catch (ProductException pe)
            {
                logger.Error(pe, $"Cannot retrieve Product({input}).");
                output = "Error occured while retrieving a product.\nTry again.\n";
            }
            catch (Exception e)
            {
                logger.Fatal(e, $"Unexpected error.");
                output = "Unexpected error occured while retrieving a product.\nTry again.\n";
            }
            return output;
        }

        protected override string GetAll()
        {
            string output = "";
            try
            {
                logger.Info($"Attempt to retrieve Products...");
                List<Product> result = model.GetAll();
                logger.Info($"Products were retrieved successfully.");
                output = "Products:\n" + "+---+--------------+------+--------+----+-------------+\n" +
                       string.Format(Product.OutputPattern, "ID", "Name", "Energy", "Protein", "Fat", "Carbohydrates") +
                       "+---+--------------+------+--------+----+-------------+\n";

                foreach (var p in result)
                {
                    output += p.ToString();
                }

            }
            catch (ProductException pe)
            {
                logger.Error(pe, $"Cannot retrieve Products.");
                output = "Error occured while retreiving products.\nTry again.\n";
            }
            catch (Exception e)
            {
                logger.Fatal(e, $"Unexpected error.");
                output = "Unexpected error occured while retrieving products.\nTry again.\n";
            }
            return output;
        }

        protected override string Remove(string input)
        {
            string output = "";
            try
            {
                logger.Info($"Attempt to remove Product({input})...");
                Product result = model.Remove(int.Parse(input));
                logger.Info($"Product({input}) was removed successfully.");
                output = $"Product#({input}):\n" + "+---+--------------+------+--------+----+-------------+\n" +
                       string.Format(Product.OutputPattern, "ID", "Name", "Energy", "Protein", "Fat", "Carbohydrates") +
                       "+---+--------------+------+--------+----+-------------+\n" +
                       result.ToString();
            }
            catch (FormatException fe)
            {
                logger.Error(fe, $"Cannot covert values.");
                output = "Wrong input format.\nTry again.\n";
            }
            catch (ProductException pe)
            {
                logger.Error(pe, $"Cannot remove Product({input}).");
                output = "Error occured while removing a product.\nTry again.\n";
            }
            catch (Exception e)
            {
                logger.Fatal(e, $"Unexpected error.");
                output = "Unexpected error occured while removing a product.\nTry again.\n";
            }
            return output;
        }
    }
}
