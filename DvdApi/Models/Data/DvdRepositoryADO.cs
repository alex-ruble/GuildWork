using DvdApi.Models.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DvdApi.Models.Data
{
    public class DvdRepositoryADO : IDvdRepository
    {
        public void Create(DVD dvd)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DvdLibraryApp"].ConnectionString;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Create";
                cmd.Parameters.AddWithValue("@Title", dvd.Title);
                cmd.Parameters.AddWithValue("@RealeaseYear", dvd.RealeaseYear);
                cmd.Parameters.AddWithValue("@Director", dvd.Director);
                cmd.Parameters.AddWithValue("@Rating", dvd.Rating);
                cmd.Parameters.AddWithValue("@Notes", dvd.Notes);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int dvdId)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DvdLibraryApp"].ConnectionString;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Delete";
                cmd.Parameters.AddWithValue("@DvdId", dvdId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public DVD GetOne(int dvdId)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                DVD dvd = null;

                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DvdLibraryApp"].ConnectionString;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "GetOne";
                cmd.Parameters.AddWithValue("@DvdId", dvdId);

                conn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    dvd = new DVD();
                    dr.Read();
                    dvd.DvdId = (int)dr["DvdId"];
                    dvd.Title = dr["Title"].ToString();
                    dvd.RealeaseYear = (int)dr["RealeaseYear"];
                    dvd.Director = dr["Director"].ToString();
                    dvd.Rating = dr["Rating"].ToString();
                    dvd.Notes = dr["Notes"].ToString();
                }
                return dvd;
            }
        }

        public List<DVD> GetAll()
        {
            List<DVD> dvds = new List<DVD>();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DvdLibraryApp"].ConnectionString;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "GetAll";

                conn.Open();

                using(SqlDataReader dr = cmd.ExecuteReader())
                {
                    while(dr.Read())
                    {
                        DVD dvd = new DVD();

                        dvd.DvdId = (int)dr["DvdId"];
                        dvd.Title = dr["Title"].ToString();
                        dvd.RealeaseYear = (int)dr["RealeaseYear"];
                        dvd.Director = dr["Director"].ToString();
                        dvd.Rating = dr["Rating"].ToString();
                        dvd.Notes = dr["Notes"].ToString();

                        dvds.Add(dvd);
                    }
                }
                return dvds;
            }
        }

        public void Update(DVD dvd)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DvdLibraryApp"].ConnectionString;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "Update";
                cmd.Parameters.AddWithValue("@DvdId", dvd.DvdId);
                cmd.Parameters.AddWithValue("@Title", dvd.Title);
                cmd.Parameters.AddWithValue("@RealeaseYear", dvd.RealeaseYear);
                cmd.Parameters.AddWithValue("@Director", dvd.Director);
                cmd.Parameters.AddWithValue("@Rating", dvd.Rating);
                cmd.Parameters.AddWithValue("@Notes", dvd.Notes);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}