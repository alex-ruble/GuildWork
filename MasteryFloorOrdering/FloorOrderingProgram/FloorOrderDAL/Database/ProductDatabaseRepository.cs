using FloorOrderModels;
using FloorOrderModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorOrderDAL.Database
{
    public class ProductDatabaseRepository : IProductRepository
    {
        public List<Product> LoadAll()
        {
            List<Product> products = new List<Product>();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["FlooringMastery"].ConnectionString;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.CommandText = "GetProducts";

                conn.Open();

                using(SqlDataReader dr = cmd.ExecuteReader())
                {
                    while(dr.Read())
                    {
                        Product product = new Product();

                        product.ProductType = dr["ProductType"].ToString();
                        product.CostPerSquareFoot = (decimal)dr["CostPerSquareFoot"];
                        product.LaborCostPerSquareFoot = (decimal)dr["LaborCostSquareFoot"];

                        products.Add(product);
                    }
                }
                return products;
            }
        }

        public Product LoadOne(string productType)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                Product product = new Product();

                conn.ConnectionString = ConfigurationManager.ConnectionStrings["FlooringMastery"].ConnectionString;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "GetSingleProduct";
                cmd.Parameters.AddWithValue("@ProductType", productType);

                conn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    dr.Read();
                    product.ProductType = dr["ProductType"].ToString();
                    product.CostPerSquareFoot = (decimal)dr["CostPerSquareFoot"];
                    product.LaborCostPerSquareFoot = (decimal)dr["LaborCostSquareFoot"];
                }
                return product;
            }
        }
    }
}
