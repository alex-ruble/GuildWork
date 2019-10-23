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
    public class EditOrderWorkflow
    {
        public void Execute()
        {
            InputRules inputManager = new InputRules();
            OrderManager manager = OrderManagerFactory.Create();
            TaxStateManager stateManager = TaxStateManagerFactory.Create();
            ProductManager productManager = ProductManagerFactory.Create();
            Order order = new Order();
            DateTime orderDate;
            string stateInput, productInput, tempAreaInput;



            Console.Clear();
            Console.WriteLine("Edit an Order");
            Console.WriteLine("--------------------");



            //Get order date file
            Console.WriteLine("Please enter the Order Date you would like to edit");
            while (!DateTime.TryParse(Console.ReadLine(), out orderDate))
            {
                Console.WriteLine("That is not a valid format, please try again");
            }



            //display all orders
            OrderLookupResponse fileResponse = manager.AllOrderLookup(orderDate);
            if(fileResponse.Success)
            {
                foreach (Order entry in fileResponse.Orders)
                {
                    ConsoleIO.DisplayOrder(entry);
                }
            }
            Console.WriteLine();



            //get order number and make edits
            int orderNumberInput;
            Console.WriteLine("Enter the Order Number you'd like to edit:");
            while(!int.TryParse(Console.ReadLine(), out orderNumberInput))
            {
                Console.WriteLine("That is not a valid input.  Try again");
            }
            OrderLookupResponse response = manager.OrderLookup(orderDate, orderNumberInput);

            if (response.Success)
            {
                ConsoleIO.DisplayOrder(response.Order);
                order = response.Order;
                Console.WriteLine();

                Console.WriteLine("Please enter new values, entering nothing will keep current value.");



                //Edit Name
                NameCheckResponse nameResponse = new NameCheckResponse();
                Console.Write("Enter the Customer's Name:  ");
                string nameInput = Console.ReadLine().Replace(',', '|').Trim();
                nameResponse = inputManager.NameCheck(nameInput);
                if (nameInput != "")
                {
                    while (!nameResponse.Success)
                    {
                        nameResponse = inputManager.NameCheck(nameInput);
                        if (!nameResponse.Success)
                        {
                            Console.WriteLine(nameResponse.Message);
                            Console.Write("Name: ");
                            nameInput = Console.ReadLine();
                        }
                    }
                    order.CustomerName = nameInput;
                }
                Console.Clear();



                //Display States
                Console.WriteLine("These are the states in our system, please enter which state.");
                foreach (TaxState entry in stateManager.GetTaxStates())
                {
                    Console.WriteLine(entry.StateName);
                }

                //Edit State
                TaxStateLookupResponse stateResponse = new TaxStateLookupResponse();
                StateInputCheckResponse stateInputResponse = new StateInputCheckResponse();

                Console.WriteLine("Please enter new values, entering nothing will keep current value.");

                Console.Write("State: ");
                stateInput = Console.ReadLine();
                stateInputResponse = inputManager.StateCheck(stateInput);
                if(stateInputResponse.Success)
                {
                    stateResponse.Success = false;
                    while (!stateResponse.Success)
                    {
                        stateResponse = stateManager.FindTaxState(stateInput);
                        if (!stateResponse.Success)
                        {
                            Console.WriteLine(stateResponse.Message);
                            Console.Write("State: ");
                            stateInput = Console.ReadLine();
                        }
                    }
                    order.State = stateInput;
                }
                Console.Clear();



                //Display Products
                Console.WriteLine("These are the materials availbale to choose from:");
                foreach (Product entry in productManager.GetProducts())
                {
                    Console.WriteLine(string.Format("{0},  Labor Cost: {1},  Material Cost: {2}",
                        entry.ProductType,
                        entry.LaborCostPerSquareFoot,
                        entry.CostPerSquareFoot));
                }

                //Edit Product
                ProductLookupResposnse productResponse = new ProductLookupResposnse();
                ProductInputCheckResponse productInputResponse = new ProductInputCheckResponse();
                Console.WriteLine("Please enter new values, entering nothing will keep current value.");

                Console.Write("Product Type: ");
                productInput = Console.ReadLine();
                productInputResponse = inputManager.ProductCheck(productInput);
                if (productInputResponse.Success)
                {
                    productResponse.Success = false;
                    while (!productResponse.Success)
                    {
                        productResponse = productManager.FindProduct(productInput);
                        if (!productResponse.Success)
                        {
                            Console.WriteLine(productResponse.Message);
                            Console.Write("Product Type: ");
                            productInput = Console.ReadLine();
                        }
                    }
                    order.ProductType = productInput;
                }
                Console.Clear();



                //Edit Area
                AreaCheckResponse areaResponse = new AreaCheckResponse();
                Console.Write("Area: ");
                decimal areaInput;
                tempAreaInput = Console.ReadLine();
                if (tempAreaInput != "")
                {
                    areaResponse.Success = false;
                    while(!decimal.TryParse(tempAreaInput, out areaInput))
                    {
                        Console.WriteLine("That is not a valid input.");
                        Console.Write("Area: ");
                        tempAreaInput = Console.ReadLine();
                    }
                    while(!areaResponse.Success)
                    {
                        areaResponse = inputManager.AreaCheck(areaInput);
                        Console.WriteLine(areaResponse.Message);
                    }
                    order.Area = areaInput;
                }
                Console.Clear();



                if (stateInput == "" && productInput == "" && tempAreaInput == "")
                {
                    order = OrderCreation.EditedOrder(order.OrderDate, order.OrderNumber, order.CustomerName, order.State, order.StateAbv, order.TaxRate, order.ProductType, order.Area, order.CostPerSqaureFoot, order.LaborCostPerSquareFoot, order.MaterialCost, order.LaborCost, order.Tax, order.Total);
                }
                else
                {
                    order = OrderCreation.CreateOrder(order.OrderDate, order.OrderNumber, order.CustomerName, order.State, order.ProductType, order.Area);
                }
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
                            manager.UpdateOrder(order);
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
            else
            {
                Console.WriteLine("That Order does not currently exist in our system");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }
        }
    }
}
