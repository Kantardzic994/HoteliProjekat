using FinalniTest9.Interfaces;
using FinalniTest9.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace FinalniTest9.Repository
{
    public class HotelRepository : IDisposable, IHotelRepository
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public void Create(Hotel hotel)
        {
            db.Hotels.Add(hotel);
            db.SaveChanges();
        }

        public void Delete(Hotel hotel)
        {
            db.Hotels.Remove(hotel);
            db.SaveChanges();
        }

        public IEnumerable<Hotel> GetAll()
        {
            return db.Hotels.Include(x => x.Lanac).OrderBy(x => x.GodinaOtvararanja);
        }

        public IEnumerable<Hotel> GetByEmpoyee(int minimum)
        {
            return db.Hotels.Include(x => x.Lanac).Where(x => x.BrojZaposlenih >= minimum).OrderBy(x => x.BrojZaposlenih);
        }

        public Hotel GetById(int id)
        {
            return db.Hotels.Include(x => x.Lanac).FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Hotel> GetBySize(int najmanje, int najvise)
        {
            return db.Hotels.Include(X => X.Lanac).Where(x => x.BrojSoba > najmanje && x.BrojSoba < najvise).OrderByDescending(x => x.BrojSoba);
        }

        public void Update(Hotel hotel)
        {
            db.Entry(hotel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
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