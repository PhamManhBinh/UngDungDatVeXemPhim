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
    public class FastFoodController : Controller
    {
        private CinemaDbContext db = new CinemaDbContext();

        // GET: Admin/FastFood
        public ActionResult Index(string error)
        {
            //nếu user chưa đăng nhập thì chuyển đến trang đăng nhập
            if (Session["user"] == null)
                return RedirectToAction("Login", "Member", new { area = "" });
            //nếu user đã đăng nhập mà không có quyền admin thì chuyển đến trang chủ người dùng
            if ((Session["user"] as User).Permission != "Admin")
                return RedirectToAction("Index", "Home", new { area = "" });
            ViewBag.Error = error;
            return View(db.FastFoods.ToList());
        }

        /*/ GET: Admin/FastFood/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FastFood fastFood = db.FastFoods.Find(id);
            if (fastFood == null)
            {
                return HttpNotFound();
            }
            return View(fastFood);
        }*/

        // GET: Admin/FastFood/Create
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

        // POST: Admin/FastFood/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase FoodImage,[Bind(Include = "id,TenMon,MoTa,Gia,Image")] FastFood fastFood)
        {
            if (FoodImage == null || FoodImage.ContentLength < 1)
                ModelState.AddModelError("Image", "Bạn chưa chọn tệp để tải lên");

            if (ModelState.IsValid)
            {
                if (!Directory.Exists(Server.MapPath("~/Upload/FastFood")))
                {
                    Directory.CreateDirectory(Server.MapPath("~/Upload/FastFood"));
                }
                string path = Path.Combine(Server.MapPath("~/Upload/FastFood"), Path.GetFileName(FoodImage.FileName));
                FoodImage.SaveAs(path);

                fastFood.Image = FoodImage.FileName;
                db.FastFoods.Add(fastFood);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fastFood);
        }

        // GET: Admin/FastFood/Edit/5
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
            FastFood fastFood = db.FastFoods.Find(id);
            if (fastFood == null)
            {
                return HttpNotFound();
            }
            return View(fastFood);
        }

        // POST: Admin/FastFood/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HttpPostedFileBase FoodImage,[Bind(Include = "id,TenMon,MoTa,Gia,Image")] FastFood fastFood)
        {
            if (ModelState.IsValid)
            {
                if (FoodImage != null && FoodImage.ContentLength > 0)
                {
                    if (!Directory.Exists(Server.MapPath("~/Upload/FastFood")))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/Upload/FastFood"));
                    }
                    string path = Path.Combine(Server.MapPath("~/Upload/FastFood"), Path.GetFileName(FoodImage.FileName));
                    FoodImage.SaveAs(path);

                    fastFood.Image = FoodImage.FileName;
                }

                db.Entry(fastFood).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fastFood);
        }

        // GET: Admin/FastFood/Delete/5
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
            FastFood fastFood = db.FastFoods.Find(id);
            if (fastFood == null)
            {
                return HttpNotFound();
            }
            return View(fastFood);
        }

        // POST: Admin/FastFood/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try { 
            FastFood fastFood = db.FastFoods.Find(id);
            db.FastFoods.Remove(fastFood);
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
