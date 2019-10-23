using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using FloorOrderModels.Responses;
using FloorOrderBLL;
using FloorOrderBLL.Manager;
using FloorOrderModels;
using FloorOrderBLL.ManagerFactory;

namespace FloorOrderingUI.Workflows
{
    public class AddOrderWorkflow
    {
        public void Execute()
        {
            OrderManager manager = OrderManagerFactory.Create();
            TaxStateManager stateManager = TaxStateManagerFactory.Create();
            ProductManager productManager = ProductManagerFactory.Create();
            InputRules inputManager = new InputRules();
            Order order = new Order();

            DateTime orderDate = DateTime.MinValue;

            Console.Clear();
            Console.WriteLine("Add an Order");
            Console.WriteLine("--------------------");


            //GET ORDER DATE
            OrderLookupResponse response = new OrderLookupResponse();
            DateCheckResponse dateResponse = new DateCheckResponse();
            dateResponse.Success = false;
            int orderNumber = 1;
            while (!dateResponse.Success)
            {
                Console.WriteLine("Please enter the Order Date you would like to add:");

                while (!DateTime.TryParse(Console.ReadLine(), out orderDate))
                {
                    Console.WriteLine("That is not a valid format, please try again");
                }
                dateResponse = inputManager.DateCheck(orderDate);
                Console.WriteLine(dateResponse.Message);
            }
            
            response = manager.AllOrderLookup(orderDate);

            if (response.Success)
            {
                foreach(Order entry in response.Orders)
                {
                    ConsoleIO.DisplayOrder(entry);
                }
                Console.WriteLine();
                Console.WriteLine("Here is the current status of this Order Date");
                if (response.Orders != null)
                {
                    orderNumber = response.Orders.LastOrDefault().OrderNumber + 1;
                }
            }


            //GET CUSTOMER NAME
            string nameInput = null;
            NameCheckResponse nameResponse = new NameCheckResponse();
            nameResponse.Success = false;
            
            while (!nameResponse.Success)
            {
                Console.Write("Enter the Customer's Name:  ");
                nameInput = Console.ReadLine().Replace(',', '|').Trim();
                nameResponse = inputManager.NameCheck(nameInput);
                Console.WriteLine(nameResponse.Message);
            }
            Console.Clear();


            //GET STATE
            string stateInput = null;
            Console.WriteLine("These are the states in our system, please enter which state.");
            foreach (TaxState state in stateManager.GetTaxStates())
            {
                Console.WriteLine(state.StateName + ", " + state.StateCode);
            }

            TaxStateLookupResponse stateResponse = new TaxStateLookupResponse();
            StateInputCheckResponse stateInputResponse = new StateInputCheckResponse();
            stateResponse.Success = false;
            stateInputResponse.Success = false;

            while (!stateResponse.Success)
            {
                while (!stateInputResponse.Success)
                {
                    Console.Write("Enter the State:  ");
                    stateInput = Console.ReadLine();
                    stateInputResponse = inputManager.StateCheck(stateInput);
                    Console.WriteLine(stateInputResponse.Message);
                }

                stateResponse = stateManager.FindTaxState(stateInput);
                if (!stateResponse.Success)
                {
                    Console.WriteLine(stateResponse.Message);
                    Console.Write("Enter the State:  ");
                    stateInput = Console.ReadLine();
                }
            }
            Console.Clear();


            //GET PRODUCT MATERIAL
            string productInput = null;
            Console.WriteLine("These are the materials availbale to choose from:");
            foreach (Product product in productManager.GetProducts())
            {
                Console.WriteLine(string.Format("{0},  Labor Cost: {1},  Material Cost: {2}",
                    product.ProductType,
                    product.LaborCostPerSquareFoot,
                    product.CostPerSquareFoot));
            }
            ProductLookupResposnse productResponse = new ProductLookupResposnse();
            ProductInputCheckResponse productInputResponse = new ProductInputCheckResponse();
            productResponse.Success = false;
            productInputResponse.Success = false;

            while (!productResponse.Success)
            {
                while (!productInputResponse.Success)
                {
                    Console.Write("Enter the Type of materials:  ");
                    productInput = Console.ReadLine();
                    productInputResponse = inputManager.ProductCheck(productInput);
                    Console.WriteLine(productInputResponse.Message);
                }

                productResponse = productManager.FindProduct(productInput);
                if (!productResponse.Success)
                {
                    Console.WriteLine(productResponse.Message);
                    Console.Write("Enter the Type of materials:  ");
                    productInput = Console.ReadLine();
                }
            }
            Console.Clear();


            //GET AREA
            decimal areaInput = 0;
            AreaCheckResponse areaResponse = new AreaCheckResponse();
            areaResponse.Success = false;
            while (!areaResponse.Success)
            {
                Console.Write("Enter the square footage of the Area:  ");
                while (!decimal.TryParse(Console.ReadLine(), out areaInput))
                {
                    Console.WriteLine("That is not a valid input.");
                }
                areaResponse = inputManager.AreaCheck(areaInput);
                Console.WriteLine(areaResponse.Message);
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();


            order = OrderCreation.CreateOrder(orderDate, orderNumber, nameInput, stateInput, productInput, areaInput);
            Console.Clear();
            ConsoleIO.DisplayOrder(order);
            Console.WriteLine();
            string answer = null;
            while (answer == null)
            {
                Console.WriteLine("Do you want to save this order? (Y/N)");
                answer = Console.ReadLine().ToUpper();
                switch (answer)
                {
                    case "Y":
                        manager.CreateOrder(order);
                        Console.WriteLine("Order Saved");
                        break;
                    case "N":
                        Console.WriteLine("Order Not Saved");
                        break;
                    default:
                        Console.WriteLine("That is not a valid input, try again");
                        answer = null;
                        break;
                }
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            return;
            
        }
    }
}
