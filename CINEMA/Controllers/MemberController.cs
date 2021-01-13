using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CINEMA.EF;
using CINEMA.Models;

namespace CINEMA.Controllers
{
    public class MemberController : Controller
    {
        //
        // GET: /Member/
        UserDAO userDAO = new UserDAO();

        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                TempData["Message"] = "Vui lòng đăng nhập trước";
                TempData["Url"] = Request.Url;
                return RedirectToAction("Login");
            }
            return View(Session["user"]);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Index([Bind(Include = "id,Email,Password,Name,Phone,Gender,Birthday,Permission")] User user)
        {
            if (Session["user"] == null)
            {
                TempData["Message"] = "Vui lòng đăng nhập trước";
                TempData["Url"] = Request.Url;
                return RedirectToAction("Login");
            }
            if (ModelState.IsValid)
            {
                userDAO.UpdateUser(user);
                Session["user"] = user;
            }

            return View(Session["user"]);
        }

        public ActionResult Login()
        {
            if (Session["user"] != null)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Message = TempData["Message"];
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                if (userDAO.Login(model))
                {
                    var user = userDAO.GetByEmail(model.Email);
                    Session.Add("user", user);

                    if (user.Permission == "Admin")
                        return RedirectToAction("Index", "Home", new { area = "Admin" });

                    if (TempData["Url"] != null)
                        return Redirect(TempData["Url"].ToString());
 

                    return RedirectToAction("Index","Home");
                }
                else
                {
                    ModelState.AddModelError("", "Email hoặc mật khẩu không đúng!");
                }
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session["user"] = null;
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Register()
        {
            if (Session["user"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Register(User user, string confirmPassword)
        {
            if (ModelState.IsValid)
            {
                if (confirmPassword.Trim().Length < 1)
                {
                    ModelState.AddModelError("", "Bạn chưa nhập xác nhận mật khẩu");
                }

                if (user.Password == confirmPassword)
                {
                    if (userDAO.CheckUser(user.Email))
                    {
                        ModelState.AddModelError("", "Tài khoản đã tồn tại");
                    }
                    else
                    {
                        userDAO.InsertUser(user);
                        ViewBag.Message = "Đăng ký thành công!";
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Xác nhận mật khẩu không đúng!");
                }
            }
            return View(user);
        }
        public ActionResult ChangePassword()
        {
            if (Session["user"] == null)
            {
                TempData["Url"] = Request.Url;
                TempData["Message"] = "Vui lòng đăng nhập trước";
                return RedirectToAction("Login");
            }
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(string Old_password, string New_password, string Confirm_password)
        {
            if (Old_password.Trim().Length < 1)
            {
                ViewBag.Message = "Bạn chưa nhập mật khẩu cũ";
                return View();
            }
            if (New_password.Trim().Length < 1)
            {
                ViewBag.Message = "Bạn chưa nhập mật khẩu mới";
                return View();
            }
            if (Confirm_password.Trim().Length < 1)
            {
                ViewBag.Message = "Bạn chưa xác nhận mật khẩu mới";
                return View();
            }
            if ((Session["user"] as User).Password != Old_password)
            {
                ViewBag.Message = "Mật khẩu cũ không chính xác";
                return View();
            }
            if (New_password != Confirm_password)
            {
                ViewBag.Message = "Xác nhận mật khẩu không đúng";
                return View();
            }
            User temp = Session["user"] as User;
            temp.Password = New_password;
            userDAO.UpdateUser(temp);
            ViewBag.Message = "Đổi mật khẩu thành công";
            return View();
        }
        public ActionResult CheckMail()
        {
            return View();
        }
        public ActionResult Forgot()
        {
            return View();
        }
        public ActionResult UpdatePassword()
        {
            return View();
        }
    }
}
