using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FloorOrderModels;
using FloorOrderDAL;
using FloorOrderBLL.Manager;
using FloorOrderModels.Interfaces;
using FloorOrderBLL.ManagerFactory;


namespace FloorOrderBLL
{
    public class OrderCreation
    {
        public static Order CreateOrder(DateTime orderDateInput, int orderNumberInput, string customerNameInput, string stateInput, string productTypeInput, decimal areaInput)
        {
            Conversions convert = new Conversions();
            TaxStateManager stateManager = TaxStateManagerFactory.Create();
            ProductManager productManager = ProductManagerFactory.Create();
            OrderManager manager = OrderManagerFactory.Create();
            Order order = new Order();

            order.OrderDate = orderDateInput;
            order.OrderNumber = orderNumberInput;
            order.CustomerName = customerNameInput;
            order.State = stateInput;
            order.StateAbv = stateManager.FindTaxState(stateInput).TaxState.StateCode;
            order.TaxRate = stateManager.FindTaxState(stateInput).TaxState.TaxRate;
            order.ProductType = productManager.FindProduct(productTypeInput).Product.ProductType;
            order.Area = areaInput;
            order.CostPerSqaureFoot = productManager.FindProduct(productTypeInput).Product.CostPerSquareFoot;
            order.LaborCostPerSquareFoot = productManager.FindProduct(productTypeInput).Product.LaborCostPerSquareFoot;
            order.MaterialCost = convert.MaterialCost(order, productManager.FindProduct(productTypeInput).Product);
            order.LaborCost = convert.LaborCost(order, productManager.FindProduct(productTypeInput).Product);
            order.Tax = convert.Tax(order, stateManager.FindTaxState(stateInput).TaxState);
            order.Total = convert.Total(order);

            return order;
        }

        public static Order EditedOrder(DateTime orderDate, int orderNumber, string customerName, string state, string stateAbv, decimal taxRate, string productType, decimal area, decimal costSqFt, decimal laborSqFt, decimal matCost, decimal laborCost, decimal tax, decimal total)
        {
            Order order = new Order();

            order.OrderDate = orderDate;
            order.OrderNumber = orderNumber;
            order.CustomerName = customerName;
            order.State = state;
            order.StateAbv = stateAbv;
            order.TaxRate = taxRate;
            order.ProductType = productType;
            order.Area = area;
            order.CostPerSqaureFoot = costSqFt;
            order.LaborCostPerSquareFoot = laborSqFt;
            order.MaterialCost = matCost;
            order.LaborCost = laborCost;
            order.Tax = tax;
            order.Total = total;

            return order;
        }
    }
}
