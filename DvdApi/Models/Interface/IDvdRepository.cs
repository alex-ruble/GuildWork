using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DvdApi.Models.Interface
{
    public interface IDvdRepository
    {
        List<DVD> GetAll();
        DVD GetOne(int dvdId);
        void Create(DVD dvd);
        void Update(DVD dvd);
        void Delete(int dvdId);
    }
}