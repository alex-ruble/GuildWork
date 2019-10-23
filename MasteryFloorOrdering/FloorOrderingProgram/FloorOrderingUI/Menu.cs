using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FloorOrderingUI.Workflows;

namespace FloorOrderingUI
{
    public class Menu
    {
        public static void Start()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Flooring Program");
                Console.WriteLine("=======================");
                Console.WriteLine("1. Display an Order");
                Console.WriteLine("2. Add an Order");
                Console.WriteLine("3. Edit and Order");
                Console.WriteLine("4. Remove an Order");

                Console.WriteLine("\nQ to Quit");
                Console.Write("\nEnter Selection: ");

                string userInput = Console.ReadLine().ToUpper();

                switch(userInput)
                {
                    case "1":
                        OrderLookupWorkflow lookup = new OrderLookupWorkflow();
                        lookup.Execute();
                        break;
                    case "2":
                        AddOrderWorkflow add = new AddOrderWorkflow();
                        add.Execute();
                        break;
                    case "3":
                        EditOrderWorkflow edit = new EditOrderWorkflow();
                        edit.Execute();
                        break;
                    case "4":
                        DeleteOrderWorkflow delete = new DeleteOrderWorkflow();
                        delete.Execute();
                        break;
                    case "Q":
                        return;
                }
            }
        }
    }
}
