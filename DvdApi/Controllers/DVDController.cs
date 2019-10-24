using DvdApi.Models;
using DvdApi.Models.Interface;
using DvdApi.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DvdApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DVDController : ApiController
    {
        private IDvdRepository _dvdRepository;

        public DVDController()
        {
            _dvdRepository = DvdRepoManager.Create();
        }

        [Route("dvds")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetAll()
        {
            return Ok(_dvdRepository.GetAll());
        }

        [Route("dvd/{id}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetOne(int id)
        {
            DVD found = _dvdRepository.GetOne(id);
            if (found == null)
            {
                return NotFound();
            }
            return Ok(found);
        }

        [Route("dvds/{searchCategory}/{searchValue}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult Search(string searchCategory, string searchValue)
        {
            List<DVD> found = new List<DVD>();

            switch (searchCategory)
            {
                case "title":
                    foreach (DVD dvd in _dvdRepository.GetAll().Where(d => d.Title == searchValue))
                    {
                        found.Add(dvd);
                    }
                    break;
                case "year":
                    foreach (DVD dvd in _dvdRepository.GetAll().Where(d => d.RealeaseYear == int.Parse(searchValue)))
                    {
                        found.Add(dvd);
                    }
                    break;
                case "rating":
                    foreach (DVD dvd in _dvdRepository.GetAll().Where(d => d.Rating == searchValue))
                    {
                        found.Add(dvd);
                    }
                    break;
                case "director":
                    foreach (DVD dvd in _dvdRepository.GetAll().Where(d => d.Director == searchValue))
                    {
                        found.Add(dvd);
                    }
                    break;
            }
            return Ok(found);
        }

        [Route("dvd")]
        [AcceptVerbs("POST")]
        public IHttpActionResult Create(DVD dvd)
        {
            _dvdRepository.Create(dvd);
            return Created($"dvd/{dvd.DvdId}", dvd);
        }

        [Route("dvd/{id}")]
        [AcceptVerbs("PUT")]
        public void Update(DVD dvd, int id)
        {
            _dvdRepository.Update(dvd);
        }

        [Route("dvd/{id}")]
        [AcceptVerbs("DELETE")]
        public void Delete(int id)
        {
            _dvdRepository.Delete(id);
        }
    }
}
