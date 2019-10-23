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
    public class TaxDatabaseRepository : ITaxRepository
    {
        public List<TaxState> LoadAll()
        {
            List<TaxState> states = new List<TaxState>();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["FlooringMastery"].ConnectionString;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.CommandText = "GetTaxStates";

                conn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        TaxState state = new TaxState();

                        state.StateCode = dr["StateAbv"].ToString();
                        state.StateName = dr["StateName"].ToString();
                        state.TaxRate = (decimal)dr["TaxRate"];

                        states.Add(state);
                    }
                }
                return states;
            }
        }

        public TaxState LoadOne(string stateName)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                TaxState state = new TaxState();

                conn.ConnectionString = ConfigurationManager.ConnectionStrings["FlooringMastery"].ConnectionString;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "GetSingleTaxState";

                cmd.Parameters.AddWithValue("@StateName", stateName);

                conn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    dr.Read();
                    state.StateName = dr["StateName"].ToString();
                    state.TaxRate = (decimal)dr["TaxRate"];
                    state.StateCode = dr["StateAbv"].ToString();
                }
                return state;
            }
        }
    }
}
