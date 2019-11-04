using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Models;
using Model.Entities;

namespace Control.Controllers
{
    public class WarehouseController : BaseController
    {
        public WarehouseModel model;
        public WarehouseController()
        {
            model = new WarehouseModel();
            isActive = true;
        }
        public override string CheckInput(string body, string command)
        {
            string output = "WarehouseController\n\n(Body format: Id|ProductName|Quantity)\n\n";
            if (command != null)
            {
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
                    default:
                        output += "Unsupported command!";
                        break;
                }
            }
            return output + GetCommands();
        }

        protected override string Add(string input)
        {
            string output = "";
            try
            {
                string[] parameters = input.Split('|');
                WarehouseItem result = model.Add(int.Parse(parameters[0]),
                                           parameters[1],
                                           int.Parse(parameters[2]));
                output = "Object was successfully created:\n" + "+---+--------------+----------+\n" +
                       string.Format(WarehouseItem.OutputPattern, "ID", "Name", "Quantity") +
                       "+---+--------------+----------+\n" +
                       result.ToString();
            }
            catch (FormatException)
            {
                output = "Wrong input format.\nTry again.\n";
            }
            catch (WarehouseItemException)
            {
                output = "Error occured while creating a WarehouseItem.\nTry again.\n";
            }
            catch (Exception)
            {
                output = "Unexpected error occured while creating a WarehouseItem.\nTry again.\n";
            }
            return output;
        }

        protected override string Change(string input)
        {
            string output = "";
            try
            {
                string[] parameters = input.Split('|');
                WarehouseItem result = model.Change(int.Parse(parameters[0]),
                                           parameters[1],
                                           int.Parse(parameters[2]));
                output = "Object was successfully changed:\n" + "+---+--------------+----------+\n" +
                       string.Format(WarehouseItem.OutputPattern, "ID", "Name", "Quantity") +
                       "+---+--------------+----------+\n" +
                       result.ToString();
            }
            catch (FormatException)
            {
                output = "Wrong input format.\nTry again.\n";
            }
            catch (WarehouseItemException)
            {
                output = "Error occured while changing a WarehouseItem.\nTry again.\n";
            }
            catch (Exception)
            {
                output = "Unexpected error occured while changing a WarehouseItem.\nTry again.\n";
            }
            return output;
        }

        protected override string Get(string input)
        {
            string output = "";
            try
            {
                WarehouseItem result = model.Get(int.Parse(input));
                output = $"WarehouseItem#({input}):\n" + "+---+--------------+----------+\n" +
                       string.Format(WarehouseItem.OutputPattern, "ID", "Name", "Quantity") +
                       "+---+--------------+----------+\n" +
                       result.ToString();
            }
            catch (FormatException)
            {
                output = "Wrong input format.\nTry again.\n";
            }
            catch (WarehouseItemException)
            {
                output = "Error occured while retrieving a WarehouseItem.\nTry again.\n";
            }
            catch (Exception)
            {
                output = "Unexpected error occured while retrieving a WarehouseItem.\nTry again.\n";
            }
            return output;
        }

        protected override string GetAll()
        {
            string output = "";
            try
            {
                List<WarehouseItem> result = model.GetAll();
                output = "Items:\n" + "+---+--------------+----------+\n" +
                       string.Format(WarehouseItem.OutputPattern, "ID", "Name", "Quantity") +
                       "+---+--------------+----------+\n";

                foreach (var p in result)
                {
                    output += p.ToString();
                }

            }
            catch (WarehouseItemException)
            {
                output = "Error occured while retrieving WarehouseItems.\nTry again.\n";
            }
            catch (Exception)
            {
                output = "Unexpected error occured while retrieving WarehouseItems.\nTry again.\n";
            }
            return output;
        }

        protected override string Remove(string input)
        {
            string output = "";
            try
            {
                WarehouseItem result = model.Remove(int.Parse(input));
                output = $"WarehouseItem#({input}):\n" + "+---+--------------+----------+\n" +
                       string.Format(WarehouseItem.OutputPattern, "ID", "Name", "Quantity") +
                       "+---+--------------+----------+\n" +
                       result.ToString();
            }
            catch (FormatException)
            {
                output = "Wrong input format.\nTry again.\n";
            }
            catch (WarehouseItemException)
            {
                output = "Error occured while removing a WarehouseItem.\nTry again.\n";
            }
            catch (Exception)
            {
                output = "Unexpected error occured while removing a WarehouseItem.\nTry again.\n";
            }
            return output;
        }
    }
}
