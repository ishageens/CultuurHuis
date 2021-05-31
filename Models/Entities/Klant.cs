using System;
using System.Collections.Generic;

#nullable disable

namespace Model.Entities
{
    public partial class Klant
    {
        public Klant()
        {
            Reservaties = new HashSet<Reservatie>();
        }

        public int KlantNr { get; set; }
        public string Voornaam { get; set; }
        public string Familienaam { get; set; }
        public string Straat { get; set; }
        public string HuisNr { get; set; }
        public string Postcode { get; set; }
        public string Gemeente { get; set; }
        public string GebruikersNaam { get; set; }
        public string Paswoord { get; set; }

        public virtual ICollection<Reservatie> Reservaties { get; set; }
    }
}
