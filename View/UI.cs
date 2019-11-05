using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Control.Controllers;

namespace View
{
    class UI
    {
        public void Run()
        {
            Console.OutputEncoding = Encoding.UTF8;
            string input = "";
            while (input != "0")
            {
                Console.Clear();
                ShowHomeScreen();
                input = Console.ReadLine();
                switch (input.ToLower())
                {
                    case "1":
                        RunProduct();
                        break;
                    case "2":
                        RunDish();
                        break;
                    case "3":
                        RunWH();
                        break;
                    case "4":
                        RunReciept();
                        break;
                    case "0":
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Unsupported input!!\nPress any key...");
                        Console.ReadLine();
                        break;
                }
            }
        }

        private void RunReciept()
        {
            RecieptController rcp = new RecieptController();
            Console.Clear();
            Console.WriteLine("RecieptController\n\nCommands:\n" + rcp.GetCommands());
            string inp = null;
            string com = null;
            while (rcp.isActive)
            {
                Console.WriteLine("Command:");
                com = Console.ReadLine();
                Console.WriteLine("Request body:");
                inp = Console.ReadLine();
                Console.Clear();
                Console.WriteLine(rcp.CheckInput(inp, com));
            }
        }

        private void RunProduct()
        {
            ProductController prd = new ProductController();
            Console.Clear();
            Console.WriteLine("ProductController\n\nCommands:\n" + prd.GetCommands());
            string inp = null;
            string com = null;
            while (prd.isActive)
            {
                Console.WriteLine("Command:");
                com = Console.ReadLine();
                Console.WriteLine("Request body:");
                inp = Console.ReadLine();
                Console.Clear();
                Console.WriteLine(prd.CheckInput(inp, com));
            }
        }

        private void RunWH()
        {
            WarehouseController whc = new WarehouseController();
            Console.Clear();
            string inp = null;
            string com = null;
            Console.WriteLine(whc.CheckInput(inp, com));
            do
            {
                Console.WriteLine("Command:");
                com = Console.ReadLine();
                Console.WriteLine("Request body:");
                inp = Console.ReadLine();
                Console.Clear();
                Console.WriteLine(whc.CheckInput(inp, com));
            }
            while (whc.isActive);
        }

        private void RunDish()
        {
            KitchenController dsh = new KitchenController();
            Console.Clear();
            Console.WriteLine("KitchenController\n\nCommands:\n" + dsh.GetCommands());
            string inp = null;
            string com = null;
            while (dsh.isActive)
            {
                Console.WriteLine("Command:");
                com = Console.ReadLine();
                Console.WriteLine("Request body:");
                inp = Console.ReadLine();
                Console.Clear();
                Console.WriteLine(dsh.CheckInput(inp, com));
            }
        }


        private void ShowHomeScreen()
        {
            Console.WriteLine("\n1. Product controller;\n2. Kitchen (dish) controller;\n3. Warehouse controller;\n4. Reciept controller;\n0. Exit.");
        }
    }
}
