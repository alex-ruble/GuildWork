using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorOrderModels.Responses
{
    public class OrderLookupResponse : Response
    {
        public Order Order { get; set; }
        public List<Order> Orders { get; set; }
    }
    public class ProductLookupResposnse : Response
    {
        public Product Product { get; set; }
    }

    public class TaxStateLookupResponse : Response
    {
        public TaxState TaxState { get; set; }
    }
}
