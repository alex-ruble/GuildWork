using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FloorOrderModels;
using FloorOrderModels.Responses;
using FloorOrderModels.Interfaces;

namespace FloorOrderBLL.Manager
{
    public class ProductManager
    {
        private IProductRepository _productRepository;

        public ProductManager(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        
        public List<Product> GetProducts()
        {
            List<Product> products = _productRepository.LoadAll();
            return products;
        }

        public ProductLookupResposnse FindProduct(string productType)
        {
            ProductLookupResposnse response = new ProductLookupResposnse();

            response.Product = _productRepository.LoadOne(productType);

            if (response.Product == null)
            {
                response.Success = false;
                response.Message = $"{productType} is not amoung the list of availble matrials.";
            }
            else
            {
                response.Success = true;
            }
            return response;
        }
    }
}
