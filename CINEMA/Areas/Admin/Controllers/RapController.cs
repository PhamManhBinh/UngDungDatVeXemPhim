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
    public class RapController : Controller
    {
        private CinemaDbContext db = new CinemaDbContext();

        // GET: Admin/Rap
        public ActionResult Index(string error)
        {
            //nếu user chưa đăng nhập thì chuyển đến trang đăng nhập
            if (Session["user"] == null)
                return RedirectToAction("Login", "Member", new { area = "" });
            //nếu user đã đăng nhập mà không có quyền admin thì chuyển đến trang chủ người dùng
            if ((Session["user"] as User).Permission != "Admin")
                return RedirectToAction("Index", "Home", new { area = "" });

            var raps = db.Raps.Include(r => r.CumRap);

            ViewBag.Error = error;
            return View(raps.ToList());
        }
        
        // GET: Admin/Rap/Create
        public ActionResult Create()
        {
            //nếu user chưa đăng nhập thì chuyển đến trang đăng nhập
            if (Session["user"] == null)
                return RedirectToAction("Login", "Member", new { area = "" });
            //nếu user đã đăng nhập mà không có quyền admin thì chuyển đến trang chủ người dùng
            if ((Session["user"] as User).Permission != "Admin")
                return RedirectToAction("Index", "Home", new { area = "" });

            ViewBag.CumRapId = new SelectList(db.CumRaps, "id", "TenCum");
            return View();
        }

        // POST: Admin/Rap/Create
        // tạo rạp sẽ tự động tạo ra các ghế (trigger)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,TenRap,LoaiRap,KTNgang,KTDoc,CumRapId")] Rap rap)
        {
            if (ModelState.IsValid)
            {
                db.Raps.Add(rap);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CumRapId = new SelectList(db.CumRaps, "id", "TenCum", rap.CumRapId);
            return View(rap);
        }

        // GET: Admin/Rap/Edit/5
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
            Rap rap = db.Raps.Find(id);
            if (rap == null)
            {
                return HttpNotFound();
            }
            ViewBag.CumRapId = new SelectList(db.CumRaps, "id", "TenCum", rap.CumRapId);
            return View(rap);
        }

        // POST: Admin/Rap/Edit/5
        //cập nhật rạp thì xóa các ghế trong rạp để trigger phía sql sẽ tự động tạo ghế
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,TenRap,LoaiRap,KTNgang,KTDoc,CumRapId")] Rap rap)
        {
            if (ModelState.IsValid)
            {
                //lấy list thực thể ghế trong rạp đó
                var listGhe = db.Ghes.Where(t => t.idRap == rap.id).ToList();

                //xóa tất cả các ghế trong rạp
                foreach (var item in listGhe)
                    db.Ghes.Remove((Ghe)item);

                db.Entry(rap).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CumRapId = new SelectList(db.CumRaps, "id", "TenCum", rap.CumRapId);
            return View(rap);
        }

        // GET: Admin/Rap/Delete/5
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
            Rap rap = db.Raps.Find(id);
            if (rap == null)
            {
                return HttpNotFound();
            }
            return View(rap);
        }

        // POST: Admin/Rap/Delete/5
        //xóa rạp thì xóa luôn cả các ghế trong rạp
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try { 
                //lấy thực thể rạp cần xóa
                Rap rap = db.Raps.Find(id);

                //lấy list thực thể ghế trong rạp đó
                var listGhe = db.Ghes.Where(t=>t.idRap==rap.id).ToList();

                //xóa tất cả các ghế trong rạp
                foreach (var item in listGhe)
                    db.Ghes.Remove((Ghe)item);
                //tiến hành xóa rạp
                db.Raps.Remove(rap);
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
