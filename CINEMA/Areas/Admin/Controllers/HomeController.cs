using CINEMA.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CINEMA.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        CinemaDbContext db = new CinemaDbContext();
        //
        // GET: /Admin/Home/

        public ActionResult Index()
        {
            //nếu user chưa đăng nhập thì chuyển đến trang đăng nhập
            if(Session["user"] ==null)
                return RedirectToAction("Login", "Member", new { area = "" });
            //nếu user đã đăng nhập mà không có quyền admin thì chuyển đến trang chủ người dùng
            if((Session["user"] as User).Permission!="Admin")
                return RedirectToAction("Index", "Home", new { area = "" });

            ViewBag.SoPhim = db.Phims.Count();
            ViewBag.DoanhThu = db.Ves.Sum(v =>(double?) v.TongTien) ?? 0;
            ViewBag.SoVe = db.Ves.Count();
            ViewBag.NguoiDung = db.Users.Count();
            return View();
        }
        public ActionResult Logout()
        {
            Session["user"] = null;
            return RedirectToAction("Index", "Home", new { area = "" });
        }

    }
}
