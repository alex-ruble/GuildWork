using DvdApi.Controllers;
using DvdApi.Models.Data;
using DvdApi.Models.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace DvdApi.Models.Manager
{
    public class DvdRepoManager
    {
        public static IDvdRepository Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();

            switch (mode)
            {
                case "SampleData":
                    return new DvdRepositoryMock();
                case "EntityFramework":
                    return new DvdRepositoryEF();
                case "ADO":
                    return new DvdRepositoryADO();
                default:
                    throw new Exception("Mode Value in Web Config is NOT Valid");
            }
        }
    }
}