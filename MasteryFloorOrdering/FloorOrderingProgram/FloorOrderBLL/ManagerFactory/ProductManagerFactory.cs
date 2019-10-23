using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using FloorOrderDAL;
using FloorOrderDAL.Database;
using FloorOrderBLL.Manager;

namespace FloorOrderBLL.ManagerFactory
{
    public static class ProductManagerFactory
    {
        public static ProductManager Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();

            switch (mode)
            {
                case "Test":
                    return new ProductManager(new ProductRepository());
                case "Prod":
                    return new ProductManager(new ProductRepository());
                case "DBase":
                    return new ProductManager(new ProductDatabaseRepository());
                default:
                    throw new Exception("Mode value in app config is not valid");
            }
        }
    }
}
