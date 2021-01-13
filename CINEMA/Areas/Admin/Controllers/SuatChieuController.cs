using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CINEMA.EF;

namespace CINEMA.Areas.Admin.Controllers
{
    public class SuatChieuController : Controller
    {
        private CinemaDbContext db = new CinemaDbContext();

        // GET: Admin/SuatChieu
        public ActionResult Index(string error)
        {
            //nếu user chưa đăng nhập thì chuyển đến trang đăng nhập
            if (Session["user"] == null)
                return RedirectToAction("Login", "Member", new { area = "" });
            //nếu user đã đăng nhập mà không có quyền admin thì chuyển đến trang chủ người dùng
            if ((Session["user"] as User).Permission != "Admin")
                return RedirectToAction("Index", "Home", new { area = "" });

            var suatChieux = db.SuatChieux.Include(s => s.Phim).Include(s => s.Rap);
            ViewBag.Error = error;
            return View(suatChieux.ToList());
        }

        
        // GET: Admin/SuatChieu/Create
        public ActionResult Create()
        {
            //nếu user chưa đăng nhập thì chuyển đến trang đăng nhập
            if (Session["user"] == null)
                return RedirectToAction("Login", "Member", new { area = "" });
            //nếu user đã đăng nhập mà không có quyền admin thì chuyển đến trang chủ người dùng
            if ((Session["user"] as User).Permission != "Admin")
                return RedirectToAction("Index", "Home", new { area = "" });

            ViewBag.PhimId = new SelectList(db.Phims, "id", "Ten");
            ViewBag.RapId = new SelectList(db.Raps, "id", "TenRap");
            return View();
        }

        // POST: Admin/SuatChieu/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Ngay,ThoiDiemBatDau,ThoiDiemKetThuc,GiaVe,PhimId,RapId")] SuatChieu suatChieu)
        {
            if (ModelState.IsValid)
            {
                db.SuatChieux.Add(suatChieu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PhimId = new SelectList(db.Phims, "id", "Ten", suatChieu.PhimId);
            ViewBag.RapId = new SelectList(db.Raps, "id", "TenRap", suatChieu.RapId);
            return View(suatChieu);
        }

        // GET: Admin/SuatChieu/Edit/5
        public ActionResult Edit(int? id)
        {
            //nếu user chưa đăng nhập thì chuyển đến trang đăng nhập
            if (Session["user"] == null)
                return RedirectToAction("Login", "Member", new { area = "" });
            //nếu user đã đăng nhập mà không có quyền admin thì chuyển đến trang chủ người dùng
            if ((Session["user"] as User).Permission != "Admin")
                return RedirectToAction("Index", "Home", new { area = "" });

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuatChieu suatChieu = db.SuatChieux.Find(id);
            if (suatChieu == null)
            {
                return HttpNotFound();
            }
            ViewBag.PhimId = new SelectList(db.Phims, "id", "Ten", suatChieu.PhimId);
            ViewBag.RapId = new SelectList(db.Raps, "id", "TenRap", suatChieu.RapId);
            return View(suatChieu);
        }

        // POST: Admin/SuatChieu/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Ngay,ThoiDiemBatDau,ThoiDiemKetThuc,GiaVe,PhimId,RapId")] SuatChieu suatChieu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(suatChieu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PhimId = new SelectList(db.Phims, "id", "Ten", suatChieu.PhimId);
            ViewBag.RapId = new SelectList(db.Raps, "id", "TenRap", suatChieu.RapId);
            return View(suatChieu);
        }

        // GET: Admin/SuatChieu/Delete/5
        public ActionResult Delete(int? id)
        {
            //nếu user chưa đăng nhập thì chuyển đến trang đăng nhập
            if (Session["user"] == null)
                return RedirectToAction("Login", "Member", new { area = "" });
            //nếu user đã đăng nhập mà không có quyền admin thì chuyển đến trang chủ người dùng
            if ((Session["user"] as User).Permission != "Admin")
                return RedirectToAction("Index", "Home", new { area = "" });

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuatChieu suatChieu = db.SuatChieux.Find(id);
            if (suatChieu == null)
            {
                return HttpNotFound();
            }
            return View(suatChieu);
        }

        // POST: Admin/SuatChieu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try { 
                SuatChieu suatChieu = db.SuatChieux.Find(id);
                db.SuatChieux.Remove(suatChieu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { error = "Có lỗi xảy ra!" });
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
