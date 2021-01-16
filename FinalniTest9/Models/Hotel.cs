using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalniTest9.Models
{
    public class Hotel
    {

        public int Id { get; set; }
        [Required]
        [StringLength(80)]
        public string Naziv { get; set; }
        [Required]
        [Range(1950,2020)]
        public int GodinaOtvararanja { get; set; }
        [Required]
        [Range(2,99999)]
        public int BrojZaposlenih { get; set; }
        [Range(10,999)]
        public int BrojSoba { get; set; }
        public int LanacId { get; set; }
        public Lanac Lanac { get; set; }

    }
}