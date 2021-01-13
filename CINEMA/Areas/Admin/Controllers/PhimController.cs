using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CINEMA.EF;

namespace CINEMA.Areas.Admin.Controllers
{
    public class PhimController : Controller
    {
        private CinemaDbContext db = new CinemaDbContext();

        // GET: Admin/Phim
        public ActionResult Index(string error)
        {
            //nếu user chưa đăng nhập thì chuyển đến trang đăng nhập
            if (Session["user"] == null)
                return RedirectToAction("Login", "Member", new { area = "" });
            //nếu user đã đăng nhập mà không có quyền admin thì chuyển đến trang chủ người dùng
            if ((Session["user"] as User).Permission != "Admin")
                return RedirectToAction("Index", "Home", new { area = "" });
            ViewBag.Error = error;
            return View(db.Phims.ToList());
        }

        // GET: Admin/Phim/Create
        public ActionResult Create()
        {
            //nếu user chưa đăng nhập thì chuyển đến trang đăng nhập
            if (Session["user"] == null)
                return RedirectToAction("Login", "Member", new { area = "" });
            //nếu user đã đăng nhập mà không có quyền admin thì chuyển đến trang chủ người dùng
            if ((Session["user"] as User).Permission != "Admin")
                return RedirectToAction("Index", "Home", new { area = "" });

            return View();
        }

        // POST: Admin/Phim/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase PosterImage,[Bind(Include = "id,Ten,TraiLer,ThoiLuong,DaoDien,DienVien,TheLoai,NgayCongChieu,NoiDungPhim")] Phim phim)
        {
            if(PosterImage == null || PosterImage.ContentLength<1)
                ModelState.AddModelError("Poster", "Bạn chưa chọn tệp để tải lên");

            if (ModelState.IsValid)
            {
                if (!Directory.Exists(Server.MapPath("~/Upload/Poster")))
                {
                    Directory.CreateDirectory(Server.MapPath("~/Upload/Poster"));
                }
                string path = Path.Combine(Server.MapPath("~/Upload/Poster"), Path.GetFileName(PosterImage.FileName));
                PosterImage.SaveAs(path);

                phim.Poster = PosterImage.FileName;
                db.Phims.Add(phim);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(phim);
        }

        // GET: Admin/Phim/Edit/5
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
            Phim phim = db.Phims.Find(id);
            if (phim == null)
            {
                return HttpNotFound();
            }
            return View(phim);
        }

        // POST: Admin/Phim/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HttpPostedFileBase PosterImage, [Bind(Include = "id,Ten,Poster,TraiLer,ThoiLuong,DaoDien,DienVien,TheLoai,NgayCongChieu,NoiDungPhim")] Phim phim)
        {
            if (ModelState.IsValid)
            {
                if(PosterImage != null && PosterImage.ContentLength > 0)
                {
                    if (!Directory.Exists(Server.MapPath("~/Upload/Poster")))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/Upload/Poster"));
                    }
                    string path = Path.Combine(Server.MapPath("~/Upload/Poster"), Path.GetFileName(PosterImage.FileName));
                    PosterImage.SaveAs(path);

                    phim.Poster = PosterImage.FileName;
                }
                db.Entry(phim).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(phim);
        }

        // GET: Admin/Phim/Delete/5
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
            Phim phim = db.Phims.Find(id);
            if (phim == null)
            {
                return HttpNotFound();
            }
            return View(phim);
        }

        // POST: Admin/Phim/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try { 
                Phim phim = db.Phims.Find(id);
                db.Phims.Remove(phim);
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
