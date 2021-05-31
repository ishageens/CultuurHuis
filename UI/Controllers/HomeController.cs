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


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
