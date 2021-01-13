using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CINEMA.EF;

namespace CINEMA.Controllers
{
    public class HomeController : Controller
    {
        CinemaDbContext db = new CinemaDbContext();

        // GET: /Home/
        public ActionResult Index()
        {
            ViewData["Banner"] = db.Banners.ToList();
            ViewData["PhimMoi"] = db.Phims.OrderByDescending(x=>x.NgayCongChieu).ToList();
            return View();
        }
        public ActionResult Error()
        {
            return View();
        }

    }
}
