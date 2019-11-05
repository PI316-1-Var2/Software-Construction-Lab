using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Models;
using Model.Entities;

namespace Control.Controllers
{
    public class RecieptController : BaseController
    {
        public RecieptModel model;
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
                string[] parameters = input.Split('|');
                DateTime RecieptDate = DateTime.ParseExact(parameters[5], "dd-MM-yyyy", null);
                Reciept result = model.Add(int.Parse(parameters[0]),
                                           parameters[1],
                                           int.Parse(parameters[2]),
                                           double.Parse(parameters[3]),
                                           parameters[4],
                                           RecieptDate);
                output = "Obect was successfully created:\n" + "+---+---------------------+--------+---------+---------------+----------+\n" +
                       string.Format(Reciept.OutputPattern, "Id", "Name", "Quantity", "Price", "Customer", "Date") +
                       "+---+---------------------+--------+---------+---------------+----------+\n" +
                       result.ToString();
            }
            catch (FormatException)
            {
                output = "Wrong format.\nTry again.\n";
            }
            catch (RecieptException)
            {
                output = "Error occured while creating a reciept.\nTry again.\n";
            }
            catch (Exception)
            {
                output = "Unexpected error occured while creating a reciept.\nTry again later.\n";
            }
            return output;
        }

        protected override string Change(string input)
        {
            string output = "";
            try
            {
                string[] parameters = input.Split('|');
                DateTime RecieptDate = DateTime.ParseExact(parameters[5], "dd-MM-yyyy", null);
                Reciept result = model.Change(int.Parse(parameters[0]),
                                           parameters[1],
                                           int.Parse(parameters[2]),
                                           double.Parse(parameters[3]),
                                           parameters[4],
                                           RecieptDate);
                output = "Obect was successfully change:\n" + "+---+---------------------+--------+---------+---------------+----------+\n" +
                       string.Format(Reciept.OutputPattern, "Id", "Name", "Quantity", "Price", "Customer", "Date") +
                       "+---+---------------------+--------+---------+---------------+----------+\n" +
                       result.ToString();
            }
            catch (FormatException)
            {
                output = "Wrong input format.\nTry again.\n";
            }
            catch (RecieptException)
            {
                output = "Error occured while editing a reciept.\nTry again.\n";
            }
            catch (Exception)
            {
                output = "Unexpected error occured while editing a reciept.\nTry again.\n";
            }
            return output;
        }

        protected override string Get(string input)
        {
            string output = "";
            try
            {
                Reciept result = model.Get(int.Parse(input));
                output = $"Reciept#({input}):\n" + "+---+---------------------+--------+---------+---------------+----------+\n" +
                       string.Format(Reciept.OutputPattern, "Id", "Name", "Quantity", "Price", "Customer", "Date") +
                       "+---+---------------------+--------+---------+---------------+----------+\n" +
                       result.ToString();
            }
            catch (FormatException)
            {
                output = "Wrong input format.\nTry again.\n";
            }
            catch (RecieptException)
            {
                output = "Error occured while retrieving a reciept.\nTry again.\n";
            }
            catch (Exception)
            {
                output = "Unexpected error occured while retrieving a reciept.\nTry again.\n";
            }
            return output;
        }

        protected override string GetAll()
        {
            string output = "";
            try
            {
                List<Reciept> result = model.GetAll();
                output = "Reciepts:\n" + "+---+---------------------+--------+---------+---------------+----------+\n" +
                       string.Format(Reciept.OutputPattern, "Id", "Name", "Quantity", "Price", "Customer", "Date") +
                       "+---+---------------------+--------+---------+---------------+----------+\n";

                foreach (var c in result)
                {
                    output += c.ToString();
                }

            }
            catch (FormatException)
            {
                output = "Wrong input format.\nTry again.\n";
            }
            catch (RecieptException)
            {
                output = "Error occured while retrieving reciepts.\nTry again.\n";
            }
            catch (Exception)
            {
                output = "Unexpected error occured while retrieving reciepts.\nTry again.\n";
            }
            return output;
        }

        protected override string Remove(string input)
        {
            string output = "";
            try
            {
                Reciept result = model.Remove(int.Parse(input));
                output = $"Reciept#({input}):\n" + "+---+---------------------+--------+---------+---------------+----------+\n" +
                       string.Format(Reciept.OutputPattern, "Id", "Name", "Quantity", "Price", "Customer", "Date") +
                       "+---+---------------------+--------+---------+---------------+----------+\n" +
                       result.ToString();
            }
            catch (FormatException)
            {
                output = "Wrong input format.\nTry again.\n";
            }
            catch (RecieptException)
            {
                output = "Error occured while removing a reciept.\nTry again.\n";
            }
            catch (Exception)
            {
                output = "Unexpected error occured while removing a reciept.\nTry again.\n";
            }
            return output;
        }
        protected string GetbyDate(string input)
        {
            string output = "";
            try
            {
                List<Reciept> result = model.GetbyDate(int.Parse(input));
                output = "Reciepts:\n" + "+---+---------------------+--------+---------+---------------+----------+\n" +
                       string.Format(Reciept.OutputPattern, "Id", "Name", "Quantity", "Price", "Customer", "Date") +
                       "+---+---------------------+--------+---------+---------------+----------+\n";

                foreach (var c in result)
                {
                    output += c.ToString();
                }
            }
            catch (FormatException)
            {
                output = "Wrong input format.\nTry again.\n";
            }
            catch (RecieptException)
            {
                output = "Error occured while retrieving reciepts.\nTry again.\n";
            }
            catch (Exception)
            {
                output = "Unexpected error occured while retrieving reciepts\nTry again.\n";
            }
            return output;
        }
    }
}
