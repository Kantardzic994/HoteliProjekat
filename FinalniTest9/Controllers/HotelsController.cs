using FinalniTest9.Interfaces;
using FinalniTest9.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FinalniTest9.Controllers
{
    public class HotelsController : ApiController
    {
        public HotelsController(IHotelRepository repository)
        {
            this.repository = repository;
        }

        IHotelRepository repository { get; set; }

        public IEnumerable<Hotel> Get()
        {
            return repository.GetAll();
        }

        [Authorize]
        public IHttpActionResult Get(int id)
        {
            var hotel = repository.GetById(id);
            if (hotel == null)
            {
                return NotFound();
            }
            return Ok(hotel);
        }

        [Authorize]
        public IHttpActionResult Post(Hotel hotel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            repository.Create(hotel);
            return CreatedAtRoute("DefaultApi", new { id = hotel.Id }, hotel);
        }

        [Authorize]
        public IHttpActionResult Put(int id, Hotel hotel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != hotel.Id)
            {
                return BadRequest();
            }
            try
            {
                repository.Update(hotel);
            }
            catch
            {
                return BadRequest();
            }
            return Ok(hotel);
        }

        [Authorize]
        public IHttpActionResult Delete(int id)
        {
            var hotel = repository.GetById(id);
            if (hotel == null)
            {
                return NotFound();
            }
            repository.Delete(hotel);
            return Ok();
        }

        [Authorize]
        public IEnumerable<Hotel> GetByEmpoyee(int minimum)
        {
            return repository.GetByEmpoyee(minimum);
        }

        [Authorize]
        [Route("api/kapacitet")]
        public IEnumerable<Hotel> GetBySize(int najmanje, int najvise)
        {
            return repository.GetBySize(najmanje, najvise);
        }
    }
}
