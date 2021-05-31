using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model.Entities;
using Model.Repositories;
using UI.Models;

namespace UI.Services
{
    public class CultuurHuisService
    {
        private readonly ICultuurHuisRepository repo;
        public CultuurHuisService(ICultuurHuisRepository repo)
        {
            this.repo = repo;
        }

        public List<Genre> AlleGenres()
        {
            var lijst = repo.GetAllGenres().ToList();
            return lijst;
        }

        public List<Voorstelling> VoorstellingenPerGenre(int genreId)
        {
            var lijst = repo.GetVoorstellingenPerGenre(genreId);
            return lijst;
        }

        public Voorstelling GetVoorstelling(int id)
        {
            var lijst = repo.GetVoorstelling(id);
            return lijst;
        }

        public Klant KlantZoeken(Klant klant)
        {
            var gezochteKlant = from item in repo.GetKlanten()
                                where item.GebruikersNaam == klant.GebruikersNaam
                                select item;
            var gevondenKlant = from k in gezochteKlant
                                where k.Paswoord == klant.Paswoord
                                select k;
            var k1 = gevondenKlant.Count() == 0 ? null : gezochteKlant.First();
            return k1;
        }
    }
}
