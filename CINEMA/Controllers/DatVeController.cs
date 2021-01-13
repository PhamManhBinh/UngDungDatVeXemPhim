using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CINEMA.EF;
using CINEMA.Models;
using System.Data.SqlClient;

namespace CINEMA.Controllers
{
    public class DatVeController : Controller
    {
        CinemaDbContext db = new CinemaDbContext();
       public ActionResult Booking(int? idSC)
       {
            //check người dùng đăng nhập hay chưa
            if (Session["user"] == null)
            {
                TempData["Message"] = "Vui lòng đăng nhập trước";
                TempData["Url"] = Request.Url;
                return RedirectToAction("Login", "Member");
            }

            if(idSC==null || db.SuatChieux.Find(idSC) == null)
            {
                return HttpNotFound();
            }
            
            var suatChieu = db.SuatChieux.Find(idSC);
            var rap = suatChieu.Rap;
            var listGhe = rap.Ghes.Where(g => g.idRap == rap.id).GroupBy(g => g.Hang);
            var listFood = db.FastFoods.ToList();
            var listCTV = db.ChiTietVes.Where(v => v.Ve.SuatChieuId == suatChieu.id).ToList();
            var listGheDaDat = new List<Ghe>();
            foreach (var item in listCTV)
            {
                listGheDaDat.Add(item.Ghe);
            }

            ViewBag.SuatChieu = suatChieu;
            ViewBag.ListGhe = listGhe;
            ViewBag.ListGheDaDat = listGheDaDat;
            ViewBag.ListFood = listFood;
            return View();
       }
        [HttpPost]
        public ActionResult CheckOut(int idSuatChieu,int[] seats,FoodModel[] foods)
        {
            if (seats == null)
            {
                return View("Error");
            }

           double TongTien = 0;
            Ve ve = new Ve
            {
                SuatChieuId = idSuatChieu,
                UserId = (Session["user"] as User).id,
                ThoiDiemDatVe = DateTime.Now,
            };

            int maVe = db.Ves.Add(ve).MaVe;

                foreach (var item in seats)
                    if(item!=null)
                    {
                        db.ChiTietVes.Add(new ChiTietVe
                        {
                            MaVe = maVe,
                            MaGhe = int.Parse(item.ToString()),
                            Ghe=db.Ghes.Find(int.Parse(item.ToString())),
                            ThanhTien = Double.Parse(db.SuatChieux.Find(idSuatChieu).GiaVe.ToString()),
                            
                        });
                        TongTien += Double.Parse(db.SuatChieux.Find(idSuatChieu).GiaVe.ToString());
                    }

            if(foods != null)
                foreach (var item in foods)
                    if (item != null && item.soLuong>0)
                    {
                        db.ChiTietVe_Food.Add(new ChiTietVe_Food
                        {
                            MaVe=maVe,
                            FoodId=item.id,
                            SoLuong=item.soLuong,
                            ThanhTien=db.FastFoods.Find(item.id).Gia*item.soLuong,
                        });
                        TongTien += db.FastFoods.Find(item.id).Gia * item.soLuong;
                    }

            if (db.SaveChanges() > 0)
            {
                ViewBag.TongTien = TongTien;
                return View("Success", ve);
            }
            else
            {
                return View("Error");
            }
            return View();
        }
    }
}