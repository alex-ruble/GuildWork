using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using FloorOrderDAL;
using FloorOrderBLL.Manager;
using FloorOrderDAL.Database;

namespace FloorOrderBLL.ManagerFactory
{
    public static class OrderManagerFactory
    {
        public static OrderManager Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();

            switch(mode)
            {
                case "Test":
                    return new OrderManager(new TestOrderRepository());
                case "Prod":
                    return new OrderManager(new OrderRepository());
                case "DBase":
                    return new OrderManager(new OrderDatabaseRepository());
                default:
                    throw new Exception("Mode value in app config is not valid");
            }
        }
    }
}
