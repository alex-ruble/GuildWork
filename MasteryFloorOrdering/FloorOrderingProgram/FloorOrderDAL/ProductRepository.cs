using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using FloorOrderModels;
using FloorOrderModels.Interfaces;

namespace FloorOrderDAL
{
    public class ProductRepository : IProductRepository
    {
        public List<Product> LoadAll()
        {
            List<Product> products = new List<Product>();

            using (StreamReader sr = new StreamReader(Settings.ProductFilePath))
            {
                sr.ReadLine();
                string line = "";

                while ((line = sr.ReadLine()) != null)
                {
                    Product product = new Product();

                    string[] column = line.Split(',');

                    product.ProductType = column[0];
                    product.CostPerSquareFoot = decimal.Parse(column[1]);
                    product.LaborCostPerSquareFoot = decimal.Parse(column[2]);

                    products.Add(product);
                }
            }
            return products;
        }

        public Product LoadOne(string productType)
        {
            Product product = null;

            using (StreamReader sr = new StreamReader(Settings.ProductFilePath))
            {
                sr.ReadLine();
                string line = "";

                while ((line = sr.ReadLine()) != null)
                {
                    string[] column = line.Split(',');

                    if (column[0] == productType)
                    {
                        product = new Product();

                        product.ProductType = column[0];
                        product.CostPerSquareFoot = decimal.Parse(column[1]);
                        product.LaborCostPerSquareFoot = decimal.Parse(column[2]);
                    }
                }
            }
            return product;
        }
    }
}
