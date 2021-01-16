using FinalniTest9.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalniTest9.Interfaces
{
    public interface ILanacRepository
    {

        IEnumerable<Lanac> GetAll();
        Lanac GetById(int id);
        IEnumerable<Lanac> Tradicija();
        IEnumerable<ZaposleniDTO> Zaposleni();
        IEnumerable<SobaDTO> Sobe(int granica);
    }
}
