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
    public class ReservatieController : Controller
    {
        private readonly CultuurHuisService service;

        public ReservatieController(CultuurHuisService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult Index(int id)
        {
            var voorstelling = service.GetVoorstelling(id);
            var reservatie = new Reservatie { VoorstellingsNr = id, VoorstellingsNrNavigation = voorstelling };
            if (voorstelling.VrijePlaatsen == 0)
            {
                return View("Uitverkocht", reservatie);
            }
            else
            {
                return View(reservatie);
            }
        }

        [HttpPost]
        public IActionResult Index(Reservatie reservatie)
        {
            reservatie.VoorstellingsNrNavigation = service.GetVoorstelling(reservatie.VoorstellingsNr);
            if (reservatie.Plaatsen > reservatie.VoorstellingsNrNavigation.VrijePlaatsen)
            {
                ModelState.AddModelError(string.Empty, "Aantal plaatsen overschrijdt het maximum");
                return View(reservatie);
            }
            else
            {
                if (this.ModelState.IsValid)
                {
                    var mandje = HttpContext.Session.GetString("mandje");
                    var deSerMandje = string.IsNullOrEmpty(mandje) ? new List<Reservatie>() : JsonConvert.DeserializeObject<List<Reservatie>>(mandje);
                    deSerMandje.Add(reservatie);
                    var serMandje = JsonConvert.SerializeObject(deSerMandje);
                    HttpContext.Session.SetString("mandje", serMandje);
                    return View("ReservatieGelukt", reservatie);
                }
                else
                {
                    return View(reservatie);
                }
            }
        }
    }
}
