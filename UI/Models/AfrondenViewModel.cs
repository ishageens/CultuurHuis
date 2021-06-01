using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Models
{
    public class AfrondenViewModel
    {
        public DateTime Datum { get; set; }
        public string Titel { get; set; }
        public string Uitvoerders { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,##0.00}")]
        public decimal Prijs { get; set; }
        public short Plaatsen { get; set; }
        public string Status { get; set; }
    }
}
