using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorOrderModels.Interfaces
{
    public interface IOrderRepository
    {
        List<Order> LoadAll(DateTime orderDate);

        void SaveOrder(Order order);

        void UpdateOrder(Order order);

        Order LoadOrder(DateTime orderDate, int orderNumber);

        void DeleteOrder(Order order);
    }
}
