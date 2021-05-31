using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.ViewComponents
{
    public class ReservatieMandjeLink : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var mandje = HttpContext.Session.GetString("mandje");
            if (!string.IsNullOrEmpty(mandje))
            {
                return View();
            }
            else return Content(string.Empty);
        }

    }
}
