using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FloorOrderDAL;
using FloorOrderModels;

namespace FloorOrderBLL
{
    public class Conversions
    {
        public decimal MaterialCost(Order order, Product product)
        {
            return order.MaterialCost = order.Area * product.CostPerSquareFoot;
        }

        public decimal LaborCost(Order order, Product product)
        {
            return order.LaborCost = order.Area * product.LaborCostPerSquareFoot;
        }

        public decimal Tax(Order order, TaxState state)
        {
            return order.Tax = (order.MaterialCost + order.LaborCost) * (state.TaxRate / 100);
        }

        public decimal Total(Order order)
        {
            return order.Total = order.MaterialCost + order.LaborCost + order.Tax;
        }
    }
}
