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
    public class HomeController : Controller
    {
        private readonly CultuurHuisService service;

        public HomeController(CultuurHuisService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(service.AlleGenres());
        }

        [HttpPost]
        public IActionResult Index(Genre genre)
        {
            return RedirectToAction("VoorstellingenPerGenre", genre.GenreNr);
        }

        [Route("Voorstellingen/{id}")]
        public IActionResult VoorstellingenPerGenre(int id)
        {
            return View(service.VoorstellingenPerGenre(id));
        }

        [HttpGet]
        public IActionResult Reserveren(int id)
        {
            var voorstelling = service.GetVoorstelling(id);
            var reservatie = new Reservatie { VoorstellingsNr = id, VoorstellingsNrNavigation = voorstelling };
            return View(reservatie);
        }

        [HttpPost]
        public IActionResult Reserveren(Reservatie reservatie)
        {
            if (this.ModelState.IsValid)
            {
                var mandje = HttpContext.Session.GetString("mandje");
                var deSerMandje = string.IsNullOrEmpty(mandje) ? new List<Reservatie>() : JsonConvert.DeserializeObject<List<Reservatie>>(mandje);
                deSerMandje.Add(reservatie);
                var serMandje = JsonConvert.SerializeObject(mandje);
                HttpContext.Session.SetString("mandje", serMandje);
            }
            reservatie.VoorstellingsNrNavigation= service.GetVoorstelling(reservatie.VoorstellingsNr);
            return View(reservatie);
        }

        public IActionResult Reservatiemandje()
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


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
