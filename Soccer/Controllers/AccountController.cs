using System;
using System.Linq;
using System.Web.Mvc;
using Soccer.Models;

namespace Soccer.Controllers
{
    public class AccountController : Controller
    {
        private QlyBongDaEntities db = new QlyBongDaEntities();

        // GET: Account/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(NhanVien model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = db.NhanViens.FirstOrDefault(u => u.TenDangNhap == model.TenDangNhap || u.Email == model.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("", "Tên đăng nhập hoặc Email đã tồn tại.");
                    return View(model);
                }

                model.NgayTao = DateTime.Now;
                model.VaiTro = "User"; // Mặc định là User
                db.NhanViens.Add(model);
                db.SaveChanges();

                return RedirectToAction("Login");
            }

            return View(model);
        }

        // GET: Account/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string TenDangNhap, string MatKhau)
        {
            var user = db.NhanViens.FirstOrDefault(u => u.TenDangNhap == TenDangNhap && u.MatKhau == MatKhau);
            if (user != null)
            {
                Session["UserID"] = user.MaNhanVien;
                Session["UserName"] = user.TenDangNhap;
                user.LanDangNhapCuoi = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng.");
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}