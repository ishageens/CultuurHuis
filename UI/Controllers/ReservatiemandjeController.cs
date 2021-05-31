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
            if (string.IsNullOrEmpty(mandje))
            {
                var list = new List<Reservatie>();
                return View(list);
            }
            else
            {
                var list = JsonConvert.DeserializeObject<List<Reservatie>>(mandje);
                return View(list);
            }
        }

        public IActionResult Verwijderen(int id)
        {
            var mandje = HttpContext.Session.GetString("mandje");
            if (!string.IsNullOrEmpty(mandje))
            {
                var deSerLijst = JsonConvert.DeserializeObject<List<Reservatie>>(mandje);
                var reservatie = from item in deSerLijst
                                 where item.VoorstellingsNr == id
                                 select item;
                if (reservatie.Count() == 1)
                    deSerLijst.Remove(reservatie.First());
                var serLijst = JsonConvert.SerializeObject(deSerLijst);
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
                if (klant == null)
                {
                    ModelState.AddModelError(string.Empty, "De inloggegevens zijn onjuist. Probeer opnieuw");
                    return View("Index", klant);
                }
                else
                {
                    return View("Index", "Genre");
                }
            }
            else
            {
                return View("Index", klant);
            }
        }
    }
}
