using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using FloorOrderModels;
using FloorOrderModels.Interfaces;

namespace FloorOrderDAL
{
    public class OrderRepository : IOrderRepository
    {
        public List<Order> LoadAll(DateTime orderDate)
        {
            string date = orderDate.ToString("MMddyyyy");
            string filePath = (Settings.OrderFilePath + date + ".txt");

            List<Order> orders = new List<Order>();
            if (!File.Exists(filePath))
            {
                return null;
            }
            using (StreamReader sr = new StreamReader(filePath))
            {
                sr.ReadLine();
                string line = "";

                while ((line = sr.ReadLine()) != null)
                {
                    Order order = new Order();

                    string[] column = line.Split(',');

                    order.OrderNumber = int.Parse(column[0]);
                    order.CustomerName = column[1].Replace('|', ',');
                    order.State = column[2];
                    order.TaxRate = decimal.Parse(column[3]);
                    order.ProductType = column[4];
                    order.Area = decimal.Parse(column[5]);
                    order.CostPerSqaureFoot = decimal.Parse(column[6]);
                    order.LaborCostPerSquareFoot = decimal.Parse(column[7]);
                    order.MaterialCost = decimal.Parse(column[8]);
                    order.LaborCost = decimal.Parse(column[9]);
                    order.Tax = decimal.Parse(column[10]);
                    order.Total = decimal.Parse(column[11]);
                    order.OrderDate = orderDate;

                    orders.Add(order);
                }
            }
            return orders;
        }

        public void SaveOrder(Order order)
        {
            string date = order.OrderDate.ToString("MMddyyyy");
            string filePath = (Settings.OrderFilePath + date + ".txt");

            List<Order> orders = LoadAll(order.OrderDate);

            if (orders != null)
            {
                orders.RemoveAll(o => o.OrderNumber == order.OrderNumber);
            }
            else
            {
                orders = new List<Order>();
            }
            orders.Add(order);

            using (StreamWriter sw = new StreamWriter(filePath))
            {
                sw.WriteLine("OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total");
                foreach (Order entry in orders)
                {
                    sw.WriteLine(CreateCsvForOrder(entry));
                }
            }
        }

        public void UpdateOrder(Order order)
        {
            string date = order.OrderDate.ToString("MMddyyyy");
            string filePath = (Settings.OrderFilePath + date + ".txt");

            List<Order> orders = LoadAll(order.OrderDate);

            if (orders != null)
            {
                orders.RemoveAll(o => o.OrderNumber == order.OrderNumber);
            }
            else
            {
                orders = new List<Order>();
            }
            orders.Add(order);

            using (StreamWriter sw = new StreamWriter(filePath))
            {
                sw.WriteLine("OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total");
                foreach (Order entry in orders)
                {
                    sw.WriteLine(CreateCsvForOrder(entry));
                }
            }
        }

        private string CreateCsvForOrder(Order order)
        {
            return string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}",
                order.OrderNumber,
                order.CustomerName.Replace(',','|'),
                order.State,
                order.TaxRate,
                order.ProductType,
                order.Area,
                order.CostPerSqaureFoot,
                order.LaborCostPerSquareFoot,
                order.MaterialCost,
                order.LaborCost,
                order.Tax,
                order.Total);
        }

        public Order LoadOrder(DateTime orderDate, int orderNumber)
        {
            string date = orderDate.ToString("MMddyyyy");
            string filePath = (Settings.OrderFilePath + date + ".txt");

            Order order = null;

            using (StreamReader sr = new StreamReader(filePath))
            {
                sr.ReadLine();
                string line = "";

                while ((line = sr.ReadLine()) != null)
                {
                    order = new Order();

                    string[] column = line.Split(',');

                    order.OrderNumber = int.Parse(column[0]);
                    order.CustomerName = column[1].Replace('|', ',');
                    order.State = column[2];
                    order.TaxRate = decimal.Parse(column[3]);
                    order.ProductType = column[4];
                    order.Area = decimal.Parse(column[5]);
                    order.CostPerSqaureFoot = decimal.Parse(column[6]);
                    order.LaborCostPerSquareFoot = decimal.Parse(column[7]);
                    order.MaterialCost = decimal.Parse(column[8]);
                    order.LaborCost = decimal.Parse(column[9]);
                    order.Tax = decimal.Parse(column[10]);
                    order.Total = decimal.Parse(column[11]);
                }
            }
            return order;
        }
        public void DeleteOrder(Order order)
        {
            string date = order.OrderDate.ToString("MMddyyyy");
            string filePath = (Settings.OrderFilePath + date + ".txt");

            List<Order> orders = LoadAll(order.OrderDate);

            orders.RemoveAll(o => o.OrderNumber == order.OrderNumber);

            using (StreamWriter sw = new StreamWriter(filePath))
            {
                sw.WriteLine("OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total");
                foreach (Order entry in orders)
                {
                    sw.WriteLine(CreateCsvForOrder(entry));
                }
            }
        }
    }
}
