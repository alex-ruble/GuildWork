using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FloorOrderDAL;
using FloorOrderModels;
using FloorOrderModels.Responses;
using FloorOrderModels.Interfaces;
using FloorOrderBLL.ManagerFactory;

namespace FloorOrderBLL.Manager
{
    public class OrderManager
    {
        private IOrderRepository _orderRepository;

        public OrderManager(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public OrderLookupResponse OrderLookup(DateTime orderDate, int orderNumber)
        {
            OrderLookupResponse response = new OrderLookupResponse();

            List<Order> orders = _orderRepository.LoadAll(orderDate);
           
            if(orders == null)
            {
                response.Success = false;
                response.Message = $"{orderDate} does not exist.";
            }
            else
            {
                response.Order = orders.FirstOrDefault(order => order.OrderNumber == orderNumber);
                if (response.Order == null)
                {
                    response.Success = false;
                    response.Message = $"Order {orderNumber} does not exist";
                }
                else
                {
                    response.Success = true;
                }
            }
            return response;
        }
        public OrderLookupResponse AllOrderLookup(DateTime orderDate)
        {
            OrderLookupResponse response = new OrderLookupResponse();

            List<Order> orders = _orderRepository.LoadAll(orderDate);

            if (orders == null)
            {
                response.Success = false;
                response.Message = $"{orderDate} does not exist.";
            }
            else
            {
                response.Orders = orders;
                response.Success = true;
            }
            return response;
        }
        public void CreateOrder(Order order)
        {
            _orderRepository.SaveOrder(order);
            
        }

        public void UpdateOrder(Order order)
        {
            _orderRepository.UpdateOrder(order);
        }

        public void DeleteOrder(Order order)
        {
            _orderRepository.DeleteOrder(order);
        }
    }
}
