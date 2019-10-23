﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorOrderModels.Interfaces
{
    public interface IProductRepository
    {
        List<Product> LoadAll();

        Product LoadOne(string productType);
    }
}
