using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FloorOrderModels;
using FloorOrderModels.Interfaces;

namespace FloorOrderDAL
{
    public class TestOrderRepository : IOrderRepository
    {
        public List<Order> orders = new List<Order>();

        public TestOrderRepository()
        {
            orders.Add(_order);
        }

        private static Order _order = new Order
        {
            OrderNumber = 1,
            CustomerName = "Dwight",
            State = "Ohio",
            TaxRate = 6.25M,
            ProductType = "Wood",
            Area = 100.00M,
            CostPerSqaureFoot = 5.15M,
            LaborCostPerSquareFoot = 4.75M,
            MaterialCost = 515.00M,
            LaborCost = 475.00M,
            Tax = 61.88M,
            Total = 1051.88M,
            OrderDate = new DateTime(2013, 06, 01)
        };

        public List<Order> LoadAll(DateTime orderDate)
        {
            if (orderDate == _order.OrderDate)
            {
                return orders;
            }
            return new List<Order>();
        }
        public void SaveOrder(Order order)
        {
            orders.Add(order);
        }

        public void UpdateOrder(Order order)
        {
            throw new NotImplementedException();
        }
        public Order LoadOrder(DateTime orderDate, int orderNumber)
        {
            if(orderDate == _order.OrderDate)
            {
                return orders.FirstOrDefault(o => o.OrderNumber == orderNumber);
            }
            else
            {
                return null;
            }
        }
        public void DeleteOrder(Order order)
        {
            orders.RemoveAll(o => o.OrderNumber == order.OrderNumber);
        }
    }
}
