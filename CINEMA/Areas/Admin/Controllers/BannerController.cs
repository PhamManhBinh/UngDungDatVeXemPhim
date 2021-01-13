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
    public class BannerController : Controller
    {
        private CinemaDbContext db = new CinemaDbContext();

        // GET: Admin/Banner
        public ActionResult Index(string error)
        {
            //nếu user chưa đăng nhập thì chuyển đến trang đăng nhập
            if (Session["user"] == null)
                return RedirectToAction("Login", "Member", new { area = "" });
            //nếu user đã đăng nhập mà không có quyền admin thì chuyển đến trang chủ người dùng
            if ((Session["user"] as User).Permission != "Admin")
                return RedirectToAction("Index", "Home", new { area = "" });
            ViewBag.Error = error;
            return View(db.Banners.ToList());
        }

        

        // GET: Admin/Banner/Create
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

        // POST: Admin/Banner/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase BannerImage,[Bind(Include = "id,Image,Link")] Banner banner)
        {
            if (BannerImage == null || BannerImage.ContentLength < 1)
                ModelState.AddModelError("Poster", "Bạn chưa chọn tệp để tải lên");

            if (ModelState.IsValid)
            {
                if (!Directory.Exists(Server.MapPath("~/Upload/Banner")))
                {
                    Directory.CreateDirectory(Server.MapPath("~/Upload/Banner"));
                }
                string path = Path.Combine(Server.MapPath("~/Upload/Banner"), Path.GetFileName(BannerImage.FileName));
                BannerImage.SaveAs(path);

                banner.Image = BannerImage.FileName;
                db.Banners.Add(banner);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(banner);
        }

        // GET: Admin/Banner/Edit/5
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
            Banner banner = db.Banners.Find(id);
            if (banner == null)
            {
                return HttpNotFound();
            }
            return View(banner);
        }

        // POST: Admin/Banner/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HttpPostedFileBase BannerImage,[Bind(Include = "id,Image,Link")] Banner banner)
        {
            if (ModelState.IsValid)
            {
                if (BannerImage != null && BannerImage.ContentLength > 0)
                {
                    if (!Directory.Exists(Server.MapPath("~/Upload/Banner")))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/Upload/Banner"));
                    }
                    string path = Path.Combine(Server.MapPath("~/Upload/Banner"), Path.GetFileName(BannerImage.FileName));
                    BannerImage.SaveAs(path);

                    banner.Image = BannerImage.FileName;
                }

                db.Entry(banner).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(banner);
        }

        // GET: Admin/Banner/Delete/5
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
            Banner banner = db.Banners.Find(id);
            if (banner == null)
            {
                return HttpNotFound();
            }
            return View(banner);
        }

        // POST: Admin/Banner/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Banner banner = db.Banners.Find(id);
                db.Banners.Remove(banner);
                db.SaveChanges();
                return RedirectToAction("Index");
            }catch(Exception ex)
            {
                return RedirectToAction("Index", new { error = "Không thể xóa!" });
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
