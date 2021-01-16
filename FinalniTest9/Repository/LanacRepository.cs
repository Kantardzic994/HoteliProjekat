using FinalniTest9.Interfaces;
using FinalniTest9.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalniTest9.Repository
{
    public class LanacRepository : IDisposable, ILanacRepository
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public IEnumerable<Lanac> GetAll()
        {
            return db.Lanacs;
        }

        public Lanac GetById(int id)
        {
            return db.Lanacs.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<SobaDTO> Sobe(int granica)
        {
            IEnumerable<SobaDTO> rezultat = db.Hotels.GroupBy(x => x.Lanac, x => x.BrojSoba, (lanac, brojsoba) => new SobaDTO()
            {
                Lanac = lanac.Naziv,
                BrojSoba = brojsoba.Sum()
            }).Where(x => x.BrojSoba > granica).OrderBy(x => x.BrojSoba);

            return rezultat;
        }

        public IEnumerable<Lanac> Tradicija()
        {
            IEnumerable<Lanac> rezultat = db.Lanacs.OrderBy(x => x.GodinaOsnivanja);

            yield return rezultat.ElementAt(0);
            yield return rezultat.ElementAt(1);
        }

        public IEnumerable<ZaposleniDTO> Zaposleni()
        {
            IEnumerable<ZaposleniDTO> rezultat = db.Hotels.GroupBy(x => x.Lanac, x => x.BrojZaposlenih, (lanac, prosecanbrojzaposlenih) => new ZaposleniDTO()
            {

                Lanac = lanac.Naziv,
                ProsecanBrojZaposlenih = prosecanbrojzaposlenih.Average()
            }).OrderByDescending(x => x.ProsecanBrojZaposlenih);

            return rezultat;
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}