using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace FloorOrderModels
{
    public class Settings
    {
        public const string ProductFilePath = @"C:\Users\alexc\Desktop\SWG\MasteryFloorOrdering\SampleData\Products.txt";

        public const string TaxFilePath = @"C:\Users\alexc\Desktop\SWG\MasteryFloorOrdering\SampleData\Taxes.txt";

        public const string OrderFilePath = @"C:\Users\alexc\Desktop\SWG\MasteryFloorOrdering\SampleData\Orders_";

        //public const string Database = "Server=localhost;Database=FlooringMastery;Trusted_Connection=True";
    }
}
