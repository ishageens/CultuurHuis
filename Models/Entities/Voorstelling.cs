using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Model.Entities
{
    public partial class Voorstelling
    {
        public Voorstelling()
        {
            Reservaties = new HashSet<Reservatie>();
        }

        public int VoorstellingsNr { get; set; }
        public string Titel { get; set; }
        public string Uitvoerders { get; set; }
        public DateTime Datum { get; set; }
        public int GenreNr { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,##0.00}")]
        public decimal Prijs { get; set; }
        public short VrijePlaatsen { get; set; }

        public virtual Genre GenreNrNavigation { get; set; }
        public virtual ICollection<Reservatie> Reservaties { get; set; }
    }
}
