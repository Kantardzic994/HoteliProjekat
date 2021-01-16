using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalniTest9.Models
{
    public class Lanac
    {

        public int Id { get; set; }
        [Required]
        [StringLength(75)]
        public string Naziv { get; set; }
        [Required]
        [Range(1850,2010)]
        public int GodinaOsnivanja { get; set; }

    }
}