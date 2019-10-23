using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FloorOrderModels;
using FloorOrderModels.Responses;
using FloorOrderBLL;
using FloorOrderBLL.Manager;
using FloorOrderBLL.ManagerFactory;

namespace FloorOrderingUI.Workflows
{
    public class OrderLookupWorkflow
    {
        public void Execute()
        {
            OrderManager manager = OrderManagerFactory.Create();

            DateTime orderDate;

            Console.Clear();
            Console.WriteLine("Lookup an Order");
            Console.WriteLine("----------------------");
            Console.Write("Enter the Order Date:  ");
            while(!DateTime.TryParse(Console.ReadLine(), out orderDate))
            {
                Console.WriteLine("That is not a valid format, please try again");
            }

            OrderLookupResponse response = manager.AllOrderLookup(orderDate);

            if(response.Success)
            {
                foreach (Order order in response.Orders)
                {
                    ConsoleIO.DisplayOrder(order);
                }
            }
            else
            {
                Console.WriteLine("An error has occured: ");
                Console.WriteLine(response.Message);
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            return;
        }
    }
}
