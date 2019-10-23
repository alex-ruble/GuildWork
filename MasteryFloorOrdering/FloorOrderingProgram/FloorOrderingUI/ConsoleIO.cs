using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FloorOrderModels;

namespace FloorOrderingUI
{
    public class ConsoleIO
    {
        public static void DisplayOrder(Order order)
        {
            Console.WriteLine();
            Console.WriteLine($"Order Number:  {order.OrderNumber}");
            Console.WriteLine($"Cusomter Name:  {order.CustomerName.Replace('|', ',')}");
            Console.WriteLine($"State:  {order.State}");
            Console.WriteLine($"Product:  {order.ProductType}");
            Console.WriteLine($"Area: {order.Area}");
            Console.WriteLine($"Materials Cost:  {order.MaterialCost:c}");
            Console.WriteLine($"Labor Cost:  {order.LaborCost:c}");
            Console.WriteLine($"Tax:  {order.Tax:c}");
            Console.WriteLine($"Total:  {order.Total:c}");
        }
    }
}
