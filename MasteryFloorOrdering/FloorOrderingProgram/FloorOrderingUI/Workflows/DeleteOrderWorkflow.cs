using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FloorOrderModels.Responses;
using FloorOrderBLL;
using FloorOrderBLL.Manager;
using FloorOrderBLL.ManagerFactory;
using FloorOrderModels;

namespace FloorOrderingUI.Workflows
{
    public class DeleteOrderWorkflow
    {
        public void Execute()
        {
            OrderManager manager = OrderManagerFactory.Create();
            Order order = new Order();
            DateTime orderDate;

            Console.Clear();
            Console.WriteLine("Add an Order");
            Console.WriteLine("--------------------");
            Console.WriteLine("Please enter the Order Date you would like to add");
            while (!DateTime.TryParse(Console.ReadLine(), out orderDate))
            {
                Console.WriteLine("That is not a valid format, please try again");
            }

            OrderLookupResponse fileResponse = manager.AllOrderLookup(orderDate);

            if (fileResponse.Success)
            {
                foreach (Order entry in fileResponse.Orders)
                {
                    ConsoleIO.DisplayOrder(entry);
                }

                Console.Write("Enter the Order Number of the order you wish to delete: ");
                int orderNumberInput;
                while (!int.TryParse(Console.ReadLine(), out orderNumberInput))
                {
                    Console.WriteLine("That is not a valid input.  Try again");
                }
                OrderLookupResponse response = manager.OrderLookup(orderDate, orderNumberInput);
                order = response.Order;

                if (response.Success)
                {
                    string answer = null;
                    while (answer == null)
                    {
                        Console.WriteLine("Are you sure you wish to Delete this Order? (Y/N)");
                        answer = Console.ReadLine().ToUpper();
                        switch (answer)
                        {
                            case "Y":
                                manager.DeleteOrder(order);
                                Console.WriteLine("Order Deleted");
                                Console.WriteLine("Press any key to continue...");
                                Console.ReadKey();
                                break;
                            case "N":
                                Console.WriteLine("Order Not Deleted");
                                Console.WriteLine("Press any key to continue...");
                                Console.ReadKey();
                                break;
                            default:
                                Console.WriteLine("That is not a valid input, try again");
                                answer = null;
                                break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine(response.Message);
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }
            }
            else
            {
                Console.WriteLine(fileResponse.Message);
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }
        }
    }
}
