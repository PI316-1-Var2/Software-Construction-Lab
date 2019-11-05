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
    public class KitchenController : BaseController
    {
        public KitchenModel model;
        private ILogger logger = LabLogger.GetLoggingService();
        public KitchenController()
        {
            model = new KitchenModel();
            isActive = true;
        }
        public override string CheckInput(string body, string command)
        {
            logger.Debug($"Received user input[Command:{command}, Body:{body}].");
            string output = "KitchenController\n\n(Body format: Id|Name|Product)\n\n";
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
                logger.Info($"Attempt to add Dish({input})...");
                string[] parameters = input.Split('|');
                Dish result = model.Add(int.Parse(parameters[0]),
                                           parameters[1],
                                           parameters[2]);
                logger.Info($"Dish({input}) was created successfully.");
                output = "Object was successfully created:\n" + "+---+--------------+--------------+\n" +
                       string.Format(Dish.OutputPattern, "ID", "Name", "Product") +
                       "+---+--------------+--------------+\n" +
                       result.ToString();
            }
            catch (FormatException fe)
            {
                logger.Error(fe, $"Cannot covert values.");
                output = "Wrong input format.\nTry again.\n";
            }
            catch (DishException de)
            {
                logger.Error(de, $"Cannot create Dish({input}).");
                output = "Error occured while creating a dish.\nTry again.\n";
            }
            catch (Exception e)
            {
                logger.Fatal(e, $"Unexpected error.");
                output = "Unexpected error occured while creating a dish.\nTry again.\n";
            }
            return output;
        }


        protected override string Change(string input)
        {
            string output = "";
            try
            {
                logger.Info($"Attempt to edit Dish({input})...");
                string[] parameters = input.Split('|');
                Dish result = model.Change(int.Parse(parameters[0]),
                                           parameters[1],
                                           parameters[2]);
                logger.Info($"Dish({input}) was updated successfully.");
                output = "Object was successfully changed:\n" + "+---+--------------+--------------+\n" +
                       string.Format(Dish.OutputPattern, "ID", "Name", "Product") +
                       "+---+--------------+--------------+\n" +
                       result.ToString();
            }
            catch (FormatException fe)
            {
                logger.Error(fe, $"Cannot covert values.");
                output = "Wrong input format.\nTry again.\n";
            }
            catch (DishException de)
            {
                logger.Error(de, $"Cannot edit Dish({input}).");
                output = "Error occured while changing a dish.\nTry again.\n";
            }
            catch (Exception e)
            {
                logger.Fatal(e, $"Unexpected error.");
                output = "Unexpected error occured while changing a dish.\nTry again.\n";
            }
            return output;
        }

        protected override string Get(string input)
        {
            string output = "";
            try
            {
                logger.Info($"Attempt to retrieve Dish({input})...");
                Dish result = model.Get(int.Parse(input));
                logger.Info($"Dish({input}) was retrieved successfully.");
                output = $"Dish#({input}):\n" + "+---+--------------+--------------+\n" +
                       string.Format(Dish.OutputPattern, "ID", "Name", "Product") +
                       "+---+--------------+--------------+\n" +
                       result.ToString();
            }
            catch (FormatException fe)
            {
                logger.Error(fe, $"Cannot covert values.");
                output = "Wrong input format.\nTry again.\n";
            }
            catch (DishException de)
            {
                logger.Error(de, $"Cannot retrieve Dish({input}).");
                output = "Error occured while retrieving a dish.\nTry again.\n";
            }
            catch (Exception e)
            {
                logger.Fatal(e, $"Unexpected error.");
                output = "Unexpected error occured while retrieving a dish.\nTry again.\n";
            }
            return output;
        }

        protected override string GetAll()
        {
            string output = "";
            try
            {
                logger.Info($"Attempt to retrieve Dishes...");
                List<Dish> result = model.GetAll();
                logger.Info($"Dishes were retrieved successfully.");
                output = "Dish:\n" + "+---+--------------+--------------+\n" +
                       string.Format(Dish.OutputPattern, "ID", "Name", "Product") +
                       "+---+--------------+--------------+\n";

                foreach (var d in result)
                {
                    output += d.ToString();
                }

            }
            catch (DishException de)
            {
                logger.Error(de, $"Cannot retrieve Dishes.");
                output = "Error occured while retrieving dishes.\nTry again.\n";
            }
            catch (Exception e)
            {
                logger.Fatal(e, $"Unexpected error.");
                output = "Unexpected error occured while retrieving dishes.\nTry again.\n";
            }
            return output;
        }

        protected override string Remove(string input)
        {
            string output = "";
            try
            {
                logger.Info($"Attempt to remove Dish({input})...");
                Dish result = model.Remove(int.Parse(input));
                logger.Info($"Dish({input}) was removed successfully.");
                output = $"Dish#({input}):\n" + "+---+--------------+--------------+\n" +
                       string.Format(Product.OutputPattern, "ID", "Name", "Product") +
                       "+---+--------------+--------------+\n" +
                       result.ToString();
            }
            catch (FormatException fe)
            {
                logger.Error(fe, $"Cannot covert values.");
                output = "Wrong input format.\nTry again.\n";
            }
            catch (DishException de)
            {
                logger.Error(de, $"Cannot remove Dish({input}).");
                output = "Error occured while removing a dish.\nTry again.\n";
            }
            catch (Exception e)
            {
                logger.Fatal(e, $"Unexpected error.");
                output = "Unexpected error occured while removing a dish.\nTry again.\n";
            }
            return output;
        }
    }
}
