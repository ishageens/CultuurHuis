using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repositories
{
    public interface ICultuurHuisRepository
    {
        Genre GetGenre(int id);
        Voorstelling GetVoorstelling(int id);
        Reservatie GetReservatie(int id);
        Klant GetKlant(int id);
        List<Voorstelling> GetVoorstellingenPerGenre(int genreId);
        IEnumerable<Genre> GetAllGenres();
        Reservatie Add(Reservatie reservatie);
        public IEnumerable<Klant> GetKlanten();
    }
}
