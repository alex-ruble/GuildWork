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
    public static class TaxStateManagerFactory
    {
        public static TaxStateManager Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();

            switch (mode)
            {
                case "Test":
                    return new TaxStateManager(new TaxRepository());
                case "Prod":
                    return new TaxStateManager(new TaxRepository());
                case "DBase":
                    return new TaxStateManager(new TaxDatabaseRepository());
                default:
                    throw new Exception("Mode value in app config is not valid");
            }
        }
    }
}
