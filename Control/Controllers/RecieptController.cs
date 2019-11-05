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
    public class RecieptController : BaseController
    {
        public RecieptModel model;
        private ILogger logger = LabLogger.GetLoggingService();
        public RecieptController()
        {
            model = new RecieptModel();
            isActive = true;
        }
        public override string CheckInput(string body, string command)
        {
            string output = "RecieptController\n\n(Body format: Id|Name|Quantity|Price|Customer|Date)\n";
            if (command != null)
            {
                logger.Debug($"Received user input[Command:{command}, Body:{body}].");
                switch (command.ToLower())
                {
                    case "1":
                        output += Get(body);
                        break;
                    case "2":
                        output += GetAll();
                        break;
                    case "3":
                        output += GetbyDate(body);
                        break;
                    case "4":
                        output += Add(body);
                        break;
                    case "5":
                        output += Remove(body);
                        break;
                    case "6":
                        output += Change(body);
                        break;
                    case "0":
                        isActive = false;
                        break;
                    case null:
                        break;
                    default:
                        logger.Debug("User called for non-existant function.");
                        output += "Wrong command!";
                        break;
                }
            }
            return output + GetCommands();
        }

        public override string GetCommands()
        {
            return "\n1. Get item by Id;" +
                   "\n2. Get item by Id;" +
                   "\n3. Get all items;" +
                   "\n4. Add item;" +
                   "\n5. Remove item by Id;" +
                   "\n6. Edit item;\n" +
                   "\n0. Return to home screen.\n";
        }

        protected override string Add(string input)
        {
            string output = "";
            try
            {
                logger.Info($"Attempt to add Reciept({input})...");
                string[] parameters = input.Split('|');
                DateTime RecieptDate = DateTime.ParseExact(parameters[5], "dd-MM-yyyy", null);
                Reciept result = model.Add(int.Parse(parameters[0]),
                                           parameters[1],
                                           int.Parse(parameters[2]),
                                           double.Parse(parameters[3]),
                                           parameters[4],
                                           RecieptDate);
                logger.Info($"Reciept({input}) was created successfully.");
                output = "Obect was successfully created:\n" + "+---+---------------------+--------+---------+---------------+----------+\n" +
                       string.Format(Reciept.OutputPattern, "Id", "Name", "Quantity", "Price", "Customer", "Date") +
                       "+---+---------------------+--------+---------+---------------+----------+\n" +
                       result.ToString();
            }
            catch (FormatException fe)
            {
                logger.Error(fe, $"Cannot covert values.");
                output = "Wrong format.\nTry again.\n";
            }
            catch (RecieptException re)
            {
                logger.Error(re, $"Cannot create Reciept({input}).");
                output = "Error occured while creating a reciept.\nTry again.\n";
            }
            catch (Exception e)
            {
                logger.Fatal(e, $"Unexpected error.");
                output = "Unexpected error occured while creating a reciept.\nTry again later.\n";
            }
            return output;
        }

        protected override string Change(string input)
        {
            string output = "";
            try
            {
                logger.Info($"Attempt to edit Reciept({input})...");
                string[] parameters = input.Split('|');
                DateTime RecieptDate = DateTime.ParseExact(parameters[5], "dd-MM-yyyy", null);
                Reciept result = model.Change(int.Parse(parameters[0]),
                                           parameters[1],
                                           int.Parse(parameters[2]),
                                           double.Parse(parameters[3]),
                                           parameters[4],
                                           RecieptDate);
                logger.Info($"Reciept({input}) was updated successfully.");
                output = "Obect was successfully change:\n" + "+---+---------------------+--------+---------+---------------+----------+\n" +
                       string.Format(Reciept.OutputPattern, "Id", "Name", "Quantity", "Price", "Customer", "Date") +
                       "+---+---------------------+--------+---------+---------------+----------+\n" +
                       result.ToString();
            }
            catch (FormatException fe)
            {
                logger.Error(fe, $"Cannot covert values.");
                output = "Wrong input format.\nTry again.\n";
            }
            catch (RecieptException re)
            {
                logger.Error(re, $"Cannot edit Reciept({input}).");
                output = "Error occured while editing a reciept.\nTry again.\n";
            }
            catch (Exception e)
            {
                logger.Fatal(e, $"Unexpected error.");
                output = "Unexpected error occured while editing a reciept.\nTry again.\n";
            }
            return output;
        }

        protected override string Get(string input)
        {
            string output = "";
            try
            {
                logger.Info($"Attempt to retrieve Reciept({input})...");
                Reciept result = model.Get(int.Parse(input));
                logger.Info($"Reciept({input}) was retrieved successfully.");
                output = $"Reciept#({input}):\n" + "+---+---------------------+--------+---------+---------------+----------+\n" +
                       string.Format(Reciept.OutputPattern, "Id", "Name", "Quantity", "Price", "Customer", "Date") +
                       "+---+---------------------+--------+---------+---------------+----------+\n" +
                       result.ToString();
            }
            catch (FormatException fe)
            {
                logger.Error(fe, $"Cannot covert values.");
                output = "Wrong input format.\nTry again.\n";
            }
            catch (RecieptException re)
            {
                logger.Error(re, $"Cannot retrieve Reciept({input}).");
                output = "Error occured while retrieving a reciept.\nTry again.\n";
            }
            catch (Exception e)
            {
                logger.Fatal(e, $"Unexpected error.");
                output = "Unexpected error occured while retrieving a reciept.\nTry again.\n";
            }
            return output;
        }

        protected override string GetAll()
        {
            string output = "";
            try
            {
                logger.Info($"Attempt to retrieve Reciepts...");
                List<Reciept> result = model.GetAll();
                logger.Info($"Reciepts were retrieved successfully.");
                output = "Reciepts:\n" + "+---+---------------------+--------+---------+---------------+----------+\n" +
                       string.Format(Reciept.OutputPattern, "Id", "Name", "Quantity", "Price", "Customer", "Date") +
                       "+---+---------------------+--------+---------+---------------+----------+\n";

                foreach (var c in result)
                {
                    output += c.ToString();
                }

            }
            catch (FormatException fe)
            {
                logger.Error(fe, $"Cannot covert values.");
                output = "Wrong input format.\nTry again.\n";
            }
            catch (RecieptException re)
            {
                logger.Error(re, $"Cannot retrieve Reciepts.");
                output = "Error occured while retrieving reciepts.\nTry again.\n";
            }
            catch (Exception e)
            {
                logger.Fatal(e, $"Unexpected error.");
                output = "Unexpected error occured while retrieving reciepts.\nTry again.\n";
            }
            return output;
        }

        protected override string Remove(string input)
        {
            string output = "";
            try
            {
                logger.Info($"Attempt to remove Reciept({input})...");
                Reciept result = model.Remove(int.Parse(input));
                logger.Info($"Reciept({input}) was removed successfully.");
                output = $"Reciept#({input}):\n" + "+---+---------------------+--------+---------+---------------+----------+\n" +
                       string.Format(Reciept.OutputPattern, "Id", "Name", "Quantity", "Price", "Customer", "Date") +
                       "+---+---------------------+--------+---------+---------------+----------+\n" +
                       result.ToString();
            }
            catch (FormatException fe)
            {
                logger.Error(fe, $"Cannot covert values.");
                output = "Wrong input format.\nTry again.\n";
            }
            catch (RecieptException re)
            {
                logger.Error(re, $"Cannot remove Reciept({input}).");
                output = "Error occured while removing a reciept.\nTry again.\n";
            }
            catch (Exception e)
            {
                logger.Fatal(e, $"Unexpected error.");
                output = "Unexpected error occured while removing a reciept.\nTry again.\n";
            }
            return output;
        }
        protected string GetbyDate(string input)
        {
            string output = "";
            try
            {
                logger.Info($"Attempt to retrieve Reciepts({input})...");
                List<Reciept> result = model.GetbyDate(int.Parse(input));
                logger.Info($"Reciepts({input}) were retrieved successfully.");
                output = "Reciepts:\n" + "+---+---------------------+--------+---------+---------------+----------+\n" +
                       string.Format(Reciept.OutputPattern, "Id", "Name", "Quantity", "Price", "Customer", "Date") +
                       "+---+---------------------+--------+---------+---------------+----------+\n";

                foreach (var c in result)
                {
                    output += c.ToString();
                }
            }
            catch (FormatException fe)
            {
                logger.Error(fe, $"Cannot covert values.");
                output = "Wrong input format.\nTry again.\n";
            }
            catch (RecieptException re)
            {
                logger.Error(re, $"Cannot retrieve Reciepts({input}).");
                output = "Error occured while retrieving reciepts.\nTry again.\n";
            }
            catch (Exception e)
            {
                logger.Fatal(e, $"Unexpected error.");
                output = "Unexpected error occured while retrieving reciepts\nTry again.\n";
            }
            return output;
        }
    }
}
