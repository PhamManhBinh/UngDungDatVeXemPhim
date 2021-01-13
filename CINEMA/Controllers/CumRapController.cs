using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CINEMA.EF;

namespace CINEMA.Controllers
{
    public class CumRapController : Controller
    {
        CinemaDbContext db = new CinemaDbContext();
        // GET: CumRap
        public ActionResult Index()
        {
            return View(db.CumRaps.ToList());
        }
        public ActionResult ChiTiet(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CumRap cumRap = db.CumRaps.Find(id);
            if (cumRap == null)
            {
                return HttpNotFound();
            }
            return View(cumRap);
        }
    }
}