using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using FloorOrderModels.Interfaces;
using FloorOrderModels;
using System.Configuration;

namespace FloorOrderDAL.Database
{
    public class OrderDatabaseRepository : IOrderRepository
    {
        public List<Order> LoadAll(DateTime notImportant)
        {
            //dateTime not necessary for Database Respository

            List<Order> orders = new List<Order>();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["FlooringMastery"].ConnectionString;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.CommandText = "GetAllOrders";

                conn.Open();

                using(SqlDataReader dr = cmd.ExecuteReader())
                {
                    while(dr.Read())
                    {
                        Order order = new Order();

                        order.OrderNumber = (int)dr["OrderNumber"];
                        order.CustomerName = dr["Customer"].ToString();
                        order.State = dr["StateName"].ToString();
                        order.TaxRate = (decimal)dr["TaxRate"];
                        order.ProductType = dr["ProductType"].ToString();
                        order.Area = (decimal)dr["Area"];
                        order.CostPerSqaureFoot = (decimal)dr["CostPerSquareFoot"];
                        order.LaborCostPerSquareFoot = (decimal)dr["LaborCostSquareFoot"];
                        order.MaterialCost = order.Area * order.CostPerSqaureFoot;
                        order.LaborCost = order.Area * order.LaborCostPerSquareFoot;
                        order.Tax = (order.MaterialCost + order.LaborCost) * order.TaxRate / 100;
                        order.Total = order.MaterialCost + order.LaborCost + order.TaxRate;
                        order.OrderDate = (DateTime)dr["OrderDate"];

                        orders.Add(order);
                    }
                }
                return orders;
            }
        }

        public Order LoadOrder(DateTime notImportant, int orderNumber)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                Order order = new Order();

                conn.ConnectionString = ConfigurationManager.ConnectionStrings["FlooringMastery"].ConnectionString;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "GetSingleOrder";
                cmd.Parameters.AddWithValue("@OrderNumber", orderNumber);

                conn.Open();
                
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    dr.Read();
                    order.OrderNumber = (int)dr["OrderNumber"];
                    order.CustomerName = dr["Customer"].ToString();
                    order.State = dr["StateName"].ToString();
                    order.TaxRate = (decimal)dr["TaxRate"];
                    order.ProductType = dr["ProductType"].ToString();
                    order.Area = (decimal)dr["Area"];
                    order.CostPerSqaureFoot = (decimal)dr["CostPerSquareFoot"];
                    order.LaborCostPerSquareFoot = (decimal)dr["LaborCostPerSquareFoot"];
                    order.MaterialCost = order.Area * order.CostPerSqaureFoot;
                    order.LaborCost = order.Area * order.LaborCostPerSquareFoot;
                    order.Tax = (order.MaterialCost + order.LaborCost) * order.TaxRate / 100;
                    order.Total = order.MaterialCost + order.LaborCost + order.TaxRate;
                    order.OrderDate = (DateTime)dr["OrderDate"];
                }
                return order;
            }
        }

        public void SaveOrder(Order order)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["FlooringMastery"].ConnectionString;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "AddOrder";

                cmd.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                cmd.Parameters.AddWithValue("@Name", order.CustomerName);
                cmd.Parameters.AddWithValue("@ProductType", order.ProductType);
                cmd.Parameters.AddWithValue("@StateAbv", order.StateAbv);
                cmd.Parameters.AddWithValue("@Area", order.Area);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateOrder(Order order)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["FlooringMastery"].ConnectionString;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "UpdateOrder";

                cmd.Parameters.AddWithValue("@OrderNumber", order.OrderNumber);
                cmd.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                cmd.Parameters.AddWithValue("@Name", order.CustomerName);
                cmd.Parameters.AddWithValue("@ProductType", order.ProductType);
                cmd.Parameters.AddWithValue("@StateAbv", order.StateAbv);
                cmd.Parameters.AddWithValue("@Area", order.Area);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteOrder(Order order)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["FlooringMastery"].ConnectionString;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "DeleteOrder";

                cmd.Parameters.AddWithValue("@OrderNumber", order.OrderNumber);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
