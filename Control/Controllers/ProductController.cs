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
        public ProductController(string connection)
        {
            model = new ProductModel(connection);
            isActive = true;
        }
        public override string CheckInput(string body, string command)
        {
            string output = Globalization.UIGlobalization.ProductInputHeader + "\n";
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
                logger.Error(ane, $"Such ID does not exist.");
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
            return Globalization.UIGlobalization.ProductGetCommands;
        }

        protected override string Add(string input)
        {
            logger.Info($"Attempt to add Product({input})...");
            string[] parameters = input.Split('|');
            Product result = model.Add(parameters[1],
                                       int.Parse(parameters[0]),
                                       int.Parse(parameters[2]),
                                       int.Parse(parameters[3]),
                                       int.Parse(parameters[4]));
            logger.Info($"Product({input}) was created successfully.");
            var table = CreateTable();
            AddRow(table, result);
            return Globalization.UIGlobalization.ObjectAddAlert + "\n" + table.ToString();
        }

        protected override string Change(string input)
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
            var table = CreateTable();
            AddRow(table, result);
            return Globalization.UIGlobalization.ObjectChangeAlert + "\n" + table.ToString();
        }

        protected override string Get(string input)
        {
            logger.Info($"Attempt to retrieve Product({input})...");
            Product result = model.Get(int.Parse(input));
            logger.Info($"Product({input}) was retrieved successfully.");
            var table = CreateTable();
            AddRow(table, result);
            return Globalization.UIGlobalization.ObjectGetAlert + "\n" + table.ToString();
        }

        protected override string GetAll()
        {
            logger.Info($"Attempt to retrieve Products...");
            List<Product> result = model.GetAll();
            logger.Info($"Products were retrieved successfully.");
            var table = CreateTable();
            foreach (var p in result)
            {
                AddRow(table, p);
            }
            return Globalization.UIGlobalization.ListGetAlert + "\n" + table.ToString();
        }

        protected override string Remove(string input)
        {
            logger.Info($"Attempt to remove Product({input})...");
            Product result = model.Remove(int.Parse(input));
            logger.Info($"Product({input}) was removed successfully.");
            var table = CreateTable();
            AddRow(table, result);
            return Globalization.UIGlobalization.ObjectRemoveAlert + "\n" + table.ToString();
        }

        public TableRenderer CreateTable()
        {
            var table = new TableRenderer();
            table.SetHeaders(Globalization.UIGlobalization.Id,
                             Globalization.UIGlobalization.Name,
                             Globalization.UIGlobalization.Energy,
                             Globalization.UIGlobalization.Protein,
                             Globalization.UIGlobalization.Fat,
                             Globalization.UIGlobalization.Carbohydrates);
            return table;
        }
        private void AddRow(TableRenderer table, Product item)
        {
            table.AddRow(item.ID.ToString(), item.Name.First().Name,
                         item.Energy.ToString(), item.Proteins.ToString(),
                         item.Fats.ToString(), item.Carbohydrates.ToString());
        }
    }
}
