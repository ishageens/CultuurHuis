using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        [Required(ErrorMessage = "Gebruikersnaam is verplicht.")]
        [Display(Name = "Gebruikersnaam")]
        public string GebruikersNaam { get; set; }
        [Required(ErrorMessage = "Paswoord is verplicht.")]
        [DataType(DataType.Password)]
        public string Paswoord { get; set; }

        public virtual ICollection<Reservatie> Reservaties { get; set; }
    }
}
