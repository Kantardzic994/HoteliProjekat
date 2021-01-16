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
    public class LanacsController : ApiController
    {
        public LanacsController(ILanacRepository repository)
        {
            this.repository = repository;
        }

        ILanacRepository repository { get; set; }


        public IEnumerable<Lanac> Get()
        {
            return repository.GetAll();
        }

        [Authorize]
        public IHttpActionResult Get(int id)
        {
            var lanac = repository.GetById(id);
            if (lanac == null)
            {
                return NotFound();
            }
            return Ok(lanac);
        }
        [Authorize]
        [Route("api/tradicija")]
        public IEnumerable<Lanac> GetTradicija()
        {
            return repository.Tradicija();
        }
        [Authorize]
        [Route("api/zaposleni")]
       public IEnumerable<ZaposleniDTO> GetZaposleni()
        {
            return repository.Zaposleni();
        }
        [Authorize]
        [Route("api/sobe")]
        public IEnumerable<SobaDTO> GetSoba(int granica)
        {
            return repository.Sobe(granica);
        }
    }
}
