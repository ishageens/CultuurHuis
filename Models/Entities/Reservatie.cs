using System;
using System.Collections.Generic;

#nullable disable

namespace Model.Entities
{
    public partial class Reservatie
    {
        public int ReservatieNr { get; set; }
        public int KlantNr { get; set; }
        public int VoorstellingsNr { get; set; }
        public short Plaatsen { get; set; }

        public virtual Klant KlantNrNavigation { get; set; }
        public virtual Voorstelling VoorstellingsNrNavigation { get; set; }
    }
}
