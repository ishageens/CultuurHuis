using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entities;

namespace Model.Repositories
{
    public class CultuurHuisRepository : ICultuurHuisRepository
    {
        private readonly CultuurHuisMVCContext context;
        public CultuurHuisRepository(CultuurHuisMVCContext context)
        {
            this.context = context;
        }

        public Reservatie Add(Reservatie reservatie)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Genre> GetAllGenres()
        {
            return context.Genres;
        }

        public Genre GetGenre(int id)
        {
            return context.Genres.Find(id);
        }

        public Klant GetKlant(int id)
        {
            return context.Klanten.Find(id);

        }

        public IEnumerable<Klant> GetKlanten()
        {
            return context.Klanten;
        }

        public Reservatie GetReservatie(int id)
        {
            return context.Reservaties.Find(id);
        }

        public List<Voorstelling> GetVoorstellingenPerGenre(int genreId)
        {
            return context.Voorstellingen
                .Where(g => g.GenreNr == genreId)
                .ToList();
        }

        public Voorstelling GetVoorstelling(int id)
        {
            return context.Voorstellingen.Find(id);
        }
    }
}
