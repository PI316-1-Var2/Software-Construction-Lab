using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Model.Models;
using Model.Entities;
using Logging;

namespace Control.Controllers
{
    public class RecieptController : BaseController
    {
        public RecieptModel model;
        private ILogger logger = LabLogger.GetLoggingService();
        public RecieptController(string connection)
        {
            model = new RecieptModel(connection);
            isActive = true;
        }
        public override string CheckInput(string body, string command)
        {
            string output = Globalization.UIGlobalization.RecieptInputHeader;
            try
            {
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
                        case "7":
                            output += CreateReciept(body);
                            break;
                        case "0":
                            isActive = false;
                            break;
                        case null:
                            break;
                        default:
                            logger.Debug("User called for non-existant function.");
                            output += Globalization.UIGlobalization.IncorrectCommand;
                            break;
                    }
                }
            }
            catch (FormatException fe)
            {
                logger.Error(fe, $"Failed to convert values.");
                output += Globalization.UIGlobalization.IncorrectFormatAlert;
            }
            catch (ArgumentNullException ane)
            {
                logger.Error(ane, $"Such id does not exist.");
                output += Globalization.UIGlobalization.IncorrectIdAlert;
            }
            catch (Exception e)
            {
                logger.Fatal(e, $"Unexpected error.");
                output += Globalization.UIGlobalization.UnexpectedAlert;
            }
            return output + GetCommands();
        }

        public override string GetCommands()
        {
            return Globalization.UIGlobalization.RecieptGetCommands;
        }

        protected override string Add(string input)
        {
            logger.Info($"Attempt to add Reciept({input})...");
            string[] parameters = input.Split('|');
            DateTime RecieptDate = DateTime.ParseExact(parameters[4], "dd-MM-yyyy-hh-mm-ss", CultureInfo.CurrentCulture);
            Reciept result = model.Add(int.Parse(parameters[1]),
                                       int.Parse(parameters[2]),
                                       int.Parse(parameters[3]),
                                       RecieptDate);
            logger.Info($"Reciept({input}) was created successfully.");
            var table = CreateTable();
            AddRow(table, result);
            return Globalization.UIGlobalization.ObjectAddAlert + "\n" + table.ToString();
        }

        protected override string Change(string input)
        {
            logger.Info($"Attempt to edit Reciept({input})...");
            string[] parameters = input.Split('|');
            DateTime RecieptDate = DateTime.ParseExact(parameters[4], "dd-MM-yyyy-hh-mm-ss", CultureInfo.CurrentCulture);
            Reciept result = model.Change(int.Parse(parameters[0]),
                                          int.Parse(parameters[1]),
                                          int.Parse(parameters[2]),
                                          int.Parse(parameters[3]),
                                          RecieptDate);
            logger.Info($"Reciept({input}) was updated successfully.");
            var table = CreateTable();
            AddRow(table, result);
            return Globalization.UIGlobalization.ObjectChangeAlert + "\n" + table.ToString();
        }

        protected override string Get(string input)
        {
            logger.Info($"Attempt to retrieve Reciept({input})...");
            Reciept result = model.Get(int.Parse(input));
            logger.Info($"Reciept({input}) was retrieved successfully.");
            var table = CreateTable();
            AddRow(table, result);
            return Globalization.UIGlobalization.ObjectGetAlert + "\n" + table.ToString();
        }

        protected override string GetAll()
        {
            logger.Info($"Attempt to retrieve Reciepts...");
            List<Reciept> result = model.GetAll();
            logger.Info($"Reciepts were retrieved successfully.");
            var table = CreateTable();
            foreach (var c in result)
            {
                AddRow(table, c);
            }
            return Globalization.UIGlobalization.ListGetAlert + "\n" + table.ToString();
        }

        protected override string Remove(string input)
        {
            logger.Info($"Attempt to remove Reciept({input})...");
            Reciept result = model.Remove(int.Parse(input));
            logger.Info($"Reciept({input}) was removed successfully.");
            var table = CreateTable();
            AddRow(table, result);
            return Globalization.UIGlobalization.ObjectRemoveAlert + "\n" + table.ToString();
        }

        protected string GetbyDate(string input)
        {
            logger.Info($"Attempt to retrieve Reciepts({input})...");
            List<Reciept> result = model.GetbyDate(int.Parse(input));
            logger.Info($"Reciepts({input}) were retrieved successfully.");
            var table = CreateTable();
            foreach (var c in result)
            {
                AddRow(table, c);
            }
            return Globalization.UIGlobalization.ListGetAlert + "\n" + table.ToString();
        }

        protected string CreateReciept(string input)
        {
            logger.Info($"Attempt to create Reciept({input})...");
            string[] parameters = input.Split('|');
            DateTime checkDate = DateTime.ParseExact(parameters[1], "dd-MM-yyyy-hh-mm-ss", CultureInfo.CurrentCulture);
            List<Reciept> result = model.CreateReciept(int.Parse(input), checkDate);
            logger.Info($"Reciept({input}) was created successfully.");
            var table = CreateTable();
            foreach (var c in result)
            {
                AddRow(table, c);
            }
            return Globalization.UIGlobalization.RecieptInfo + "\n" + table.ToString();
        }

        public TableRenderer CreateTable()
        {
            var table = new TableRenderer();
            table.SetHeaders(Globalization.UIGlobalization.Id,
                             Globalization.UIGlobalization.DishName,
                             Globalization.UIGlobalization.Quantity,
                             Globalization.UIGlobalization.Price,
                             Globalization.UIGlobalization.VisitorName,
                             Globalization.UIGlobalization.DateOrdered);
            return table;
        }

        private void AddRow(TableRenderer table, Reciept item)
        {
            table.AddRow(item.ID.ToString(), item.DishItem.Name.ToList().First().Name,
                         item.Quantity.ToString(), item.Price.ToString(),
                         item.User.Name, item.Date_Order.ToString());
        }
    }
}
