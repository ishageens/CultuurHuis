using System;
using System.Collections.Generic;

#nullable disable

namespace Model.Entities
{
    public partial class Genre
    {
        public Genre()
        {
            Voorstellingen = new HashSet<Voorstelling>();
        }

        public int GenreNr { get; set; }
        public string GenreNaam { get; set; }

        public virtual ICollection<Voorstelling> Voorstellingen { get; set; }
    }
}
