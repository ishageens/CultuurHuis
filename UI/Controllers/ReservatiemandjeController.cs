using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using UI.Models;
using UI.Services;

namespace UI.Controllers
{
    public class ReservatiemandjeController : Controller
    {
        private readonly CultuurHuisService service;

        public ReservatiemandjeController(CultuurHuisService service)
        {
            this.service = service;
        }
        public IActionResult Index()
        {
            var mandje = HttpContext.Session.GetString("mandje");
            var list = string.IsNullOrEmpty(mandje) ? new List<Reservatie>() : JsonConvert.DeserializeObject<List<Reservatie>>(mandje);
            return View(list);
        }

        public IActionResult Verwijderen(int id)
        {
            var mandje = HttpContext.Session.GetString("mandje");
            if (!string.IsNullOrEmpty(mandje))
            {
                var deSerLijst = JsonConvert.DeserializeObject<List<Reservatie>>(mandje);
                var lijst = service.Verwijderen(id, deSerLijst);
                var serLijst = JsonConvert.SerializeObject(lijst);
                HttpContext.Session.SetString("winkelmandje", serLijst);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Bevestigen()
        {
            return View();
        }

        public IActionResult KlantZoeken(Klant klant)
        {
            if (this.ModelState.IsValid)
            {
                var k = service.KlantZoeken(klant);
                if (k == null)
                {
                    ModelState.AddModelError(string.Empty, "De inloggegevens zijn onjuist. Probeer opnieuw");
                    return View("Bevestigen", klant);
                }
                else
                {
                    return View("Bevestigd", k);
                }
            }
            else
            {
                return View("Bevestigen", klant);
            }
        }

        public IActionResult Afronden(Klant klant)
        {
            var mandje = HttpContext.Session.GetString("mandje");
            var deSerMandje = JsonConvert.DeserializeObject<List<Reservatie>>(mandje);
            var lijstje = new List<AfrondenViewModel>();
            bool gelukt;
            foreach (var reservatie in deSerMandje)
            {
                gelukt = service.Afrekenen(klant, reservatie);
                var item = new AfrondenViewModel
                {
                    Datum = reservatie.VoorstellingsNrNavigation.Datum,
                    Titel = reservatie.VoorstellingsNrNavigation.Titel,
                    Uitvoerders = reservatie.VoorstellingsNrNavigation.Uitvoerders,
                    Prijs = reservatie.VoorstellingsNrNavigation.Prijs,
                    Plaatsen = reservatie.Plaatsen,
                    Status = gelukt == true ? "Reservatie gelukt" : "Reservatie niet gelukt"
                };
                lijstje.Add(item);
            }
            HttpContext.Session.SetString("mandje", string.Empty);
            return View(lijstje);
        }
    }
}
