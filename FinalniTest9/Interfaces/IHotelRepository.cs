using FinalniTest9.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalniTest9.Interfaces
{
   public interface IHotelRepository
    {

        IEnumerable<Hotel> GetAll();
        Hotel GetById(int id);
        IEnumerable<Hotel> GetByEmpoyee(int minimum);
        void Create(Hotel hotel);
        void Update(Hotel hotel);
        void Delete(Hotel hotel);
        IEnumerable<Hotel> GetBySize(int najmanje, int najvise);

    }
}
