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
    public class CumRapController : Controller
    {
        private CinemaDbContext db = new CinemaDbContext();

        // GET: Admin/CumRap
        public ActionResult Index(string error)
        {
            //nếu user chưa đăng nhập thì chuyển đến trang đăng nhập
            if (Session["user"] == null)
                return RedirectToAction("Login", "Member", new { area = "" });
            //nếu user đã đăng nhập mà không có quyền admin thì chuyển đến trang chủ người dùng
            if ((Session["user"] as User).Permission != "Admin")
                return RedirectToAction("Index", "Home", new { area = "" });
            ViewBag.Error = error;
            return View(db.CumRaps.ToList());
        }


        // GET: Admin/CumRap/Create
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

        // POST: Admin/CumRap/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,TenCum,DiaChi,Maps")] CumRap cumRap)
        {
            if (ModelState.IsValid)
            {
                db.CumRaps.Add(cumRap);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cumRap);
        }

        // GET: Admin/CumRap/Edit/5
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
            CumRap cumRap = db.CumRaps.Find(id);
            if (cumRap == null)
            {
                return HttpNotFound();
            }
            return View(cumRap);
        }

        // POST: Admin/CumRap/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,TenCum,DiaChi,Maps")] CumRap cumRap)
        {
            if (ModelState.IsValid)
            {
                try { 
                    db.Entry(cumRap).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Index", new { error = "Có lỗi xảy ra!" });
                }
            }
            return View(cumRap);
        }

        // GET: Admin/CumRap/Delete/5
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
            CumRap cumRap = db.CumRaps.Find(id);
            if (cumRap == null)
            {
                return HttpNotFound();
            }
            return View(cumRap);
        }

        // POST: Admin/CumRap/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                CumRap cumRap = db.CumRaps.Find(id);
                db.CumRaps.Remove(cumRap);
                db.SaveChanges();
                return RedirectToAction("Index");
            }catch(Exception ex)
            {
                return RedirectToAction("Index",new { error = "Không thể xóa" });
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
