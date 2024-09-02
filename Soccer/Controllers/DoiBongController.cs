using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Soccer.Models;

namespace Soccer.Controllers
{
    public class DoiBongController : Controller
    {
        private QlyBongDaEntities db = new QlyBongDaEntities();

        public ActionResult Index()
        {
            var doiBongs = db.DoiBongs.Include(d => d.SanVanDong).Include(d => d.GiaiDau);
            return View(doiBongs.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoiBong doiBong = db.DoiBongs.Find(id);
            if (doiBong == null)
            {
                return HttpNotFound();
            }
            return View(doiBong);
        }

        public ActionResult Create()
        {
            ViewBag.MaSanNha = new SelectList(db.SanVanDongs, "MaSan", "TenSan");
            ViewBag.MaGiaiDau = new SelectList(db.GiaiDaus, "MaGiaiDau", "TenGiaiDau");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TenDoi,ThanhPho,MaSanNha,NamThanhLap,MaGiaiDau")] DoiBong doiBong, HttpPostedFileBase logo)
        {
            if (ModelState.IsValid)
            {
                if (logo != null && logo.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(logo.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images/Logo"), fileName);
                    logo.SaveAs(path);
                    doiBong.Logo = "/Images/Logo/" + fileName;
                }

                db.DoiBongs.Add(doiBong);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaSanNha = new SelectList(db.SanVanDongs, "MaSan", "TenSan", doiBong.MaSanNha);
            ViewBag.MaGiaiDau = new SelectList(db.GiaiDaus, "MaGiaiDau", "TenGiaiDau", doiBong.MaGiaiDau);
            return View(doiBong);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoiBong doiBong = db.DoiBongs.Find(id);
            if (doiBong == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaSanNha = new SelectList(db.SanVanDongs, "MaSan", "TenSan", doiBong.MaSanNha);
            ViewBag.MaGiaiDau = new SelectList(db.GiaiDaus, "MaGiaiDau", "TenGiaiDau", doiBong.MaGiaiDau);
            return View(doiBong);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDoi,TenDoi,ThanhPho,MaSanNha,NamThanhLap,MaGiaiDau,Logo")] DoiBong doiBong, HttpPostedFileBase logo)
        {
            if (ModelState.IsValid)
            {
                if (logo != null && logo.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(logo.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images/Logo"), fileName);
                    logo.SaveAs(path);
                    doiBong.Logo = "/Images/Logo/" + fileName;
                }

                db.Entry(doiBong).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaSanNha = new SelectList(db.SanVanDongs, "MaSan", "TenSan", doiBong.MaSanNha);
            ViewBag.MaGiaiDau = new SelectList(db.GiaiDaus, "MaGiaiDau", "TenGiaiDau", doiBong.MaGiaiDau);
            return View(doiBong);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoiBong doiBong = db.DoiBongs.Find(id);
            if (doiBong == null)
            {
                return HttpNotFound();
            }
            return View(doiBong);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DoiBong doiBong = db.DoiBongs.Find(id);
            db.DoiBongs.Remove(doiBong);
            db.SaveChanges();
            return RedirectToAction("Index");
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