using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DvdApi.Models.EF;
using DvdApi.Models.Interface;

namespace DvdApi.Models.Data
{
    public class DvdRepositoryEF : IDvdRepository
    {
        DvdLibraryFramework repo = new DvdLibraryFramework();
        public void Create(DVD dvd)
        {
            repo.DVDs.Add(dvd);
            repo.SaveChanges();
        }

        public void Delete(int dvdId)
        {
            DVD dvd = repo.DVDs.FirstOrDefault(d => d.DvdId == dvdId);
            if (dvd != null)
            {
                repo.DVDs.Remove(dvd);
                repo.SaveChanges();
            }
        }

        public List<DVD> GetAll()
        {
            List<DVD> dvds = new List<DVD>();
            foreach (var dvd in repo.DVDs)
            {
                dvds.Add(dvd);
            }
            return dvds;
        }

        public DVD GetOne(int dvdId)
        {
            DVD dvd = new DVD();
            return dvd = GetAll().FirstOrDefault(d => d.DvdId == dvdId);
        }

        public void Update(DVD dvd)
        {
            repo.Entry(dvd).State = EntityState.Modified;
            repo.SaveChanges();
        }
    }
}