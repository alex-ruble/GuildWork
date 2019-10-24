using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DvdApi.Models.Interface;

namespace DvdApi.Models.Data
{
    public class DvdRepositoryMock : IDvdRepository
    {
        private static List<DVD> _dvds;

        static DvdRepositoryMock()
        {
            _dvds = new List<DVD>()
            {
                new DVD {DvdId = 0, Title = "Title1", RealeaseYear = 2018, Director = "Dir1", Rating = "G", Notes = "None"},
                new DVD {DvdId = 1, Title = "Title2", RealeaseYear = 2018, Director = "Dir2", Rating = "PG", Notes = "None"},
                new DVD {DvdId = 2, Title = "Title3", RealeaseYear = 2018, Director = "Dir3", Rating = "PG-13", Notes = "None"},
                new DVD {DvdId = 3, Title = "Title4", RealeaseYear = 2018, Director = "Dir4", Rating = "R", Notes = "None"}
            };
        }

        public List<DVD> GetAll()
        {
            return _dvds;
        }

        public DVD GetOne(int dvdId)
        {
            return _dvds.FirstOrDefault(d => d.DvdId == dvdId);
        }

        public void Create(DVD dvd)
        {
            if(_dvds.Any())
            {
                dvd.DvdId = _dvds.Max(d => d.DvdId) + 1;
            }
            else
            {
                dvd.DvdId = 0;
            }
            _dvds.Add(dvd);
        }

        public void Update(DVD dvd)
        {
            _dvds.RemoveAll(d => d.DvdId == dvd.DvdId);
            _dvds.Add(dvd);
        }

        public void Delete(int dvdId)
        {
            _dvds.RemoveAll(d => d.DvdId == dvdId);
        }
    }
}