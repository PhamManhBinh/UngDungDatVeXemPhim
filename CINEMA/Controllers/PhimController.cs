using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CINEMA.EF;

namespace CINEMA.Controllers
{
    public class PhimController : Controller
    {
        CinemaDbContext db = new CinemaDbContext();
        // GET: Phim
        public ActionResult Index()
        {
            return View(db.Phims.ToList());
        }
        public ActionResult ChiTiet(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phim phim = db.Phims.Find(id);
            if (phim == null)
            {
                return HttpNotFound();
            }
            return View(phim);
        }

        public ActionResult LichChieu(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phim phim = db.Phims.Find(id);
            if (phim == null)
            {
                return HttpNotFound();
            }
            return View(phim);
        }

        //api hiển thị lịch chiếu bằng ajax
        //cho tôi biết mã phim và ngày chiếu, tôi sẽ cho bạn biết lịch chiếu tương ứng
        public ActionResult ShowTime(DateTime? date,int? idPhim)
        {
            //điều kiện dừng
            if(date==null || idPhim==null || db.Phims.Find(idPhim)==null || db.SuatChieux.Where(x => x.Ngay == date).Count() < 1)
            {
                ViewData.Model = null;
                return View();
            }
            
            //tìm các suất chiếu của phim có id và ngày chiếu mà user yêu cầu, và nhóm các suất chiếu theo cụm rạp
            var result = db.SuatChieux.Where(x => x.PhimId == idPhim && x.Ngay == date).GroupBy(x=>x.Rap.CumRap.TenCum);

            return View(result);
        }

        public ActionResult TimKiem(string query)
        {
            var list = db.Phims.Where(p => p.Ten.Contains(query) || p.DienVien.Contains(query) || p.DaoDien.Contains(query)).ToList();
            return View("Index",list);
        }
    }
}