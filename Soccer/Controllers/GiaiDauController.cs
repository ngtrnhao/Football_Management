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
    public class GiaiDauController : Controller
    {
        private QlyBongDaEntities db = new QlyBongDaEntities();

        // GET: GiaiDau
        public ActionResult Index()
        {
            return View(db.GiaiDaus.ToList());
        }

        // GET: GiaiDau/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GiaiDau giaiDau = db.GiaiDaus.Find(id);
            if (giaiDau == null)
            {
                return HttpNotFound();
            }
            return View(giaiDau);
        }

        // GET: GiaiDau/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GiaiDau/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TenGiaiDau,QuocGia,MuaGiai,NgayBatDau,NgayKetThuc")] GiaiDau giaiDau, HttpPostedFileBase hinhAnh)
        {
            if (ModelState.IsValid)
            {
                if (hinhAnh != null && hinhAnh.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(hinhAnh.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images/GiaiDau"), fileName);
                    hinhAnh.SaveAs(path);
                    giaiDau.HinhAnh = "/Images/GiaiDau/" + fileName;
                }

                db.GiaiDaus.Add(giaiDau);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(giaiDau);
        }

        // GET: GiaiDau/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GiaiDau giaiDau = db.GiaiDaus.Find(id);
            if (giaiDau == null)
            {
                return HttpNotFound();
            }
            return View(giaiDau);
        }

        // POST: GiaiDau/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaGiaiDau,TenGiaiDau,QuocGia,MuaGiai,NgayBatDau,NgayKetThuc,HinhAnh")] GiaiDau giaiDau, HttpPostedFileBase hinhAnh)
        {
            if (ModelState.IsValid)
            {
                if (hinhAnh != null && hinhAnh.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(hinhAnh.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images/GiaiDau"), fileName);
                    hinhAnh.SaveAs(path);
                    giaiDau.HinhAnh = "/Images/GiaiDau/" + fileName;
                }

                db.Entry(giaiDau).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(giaiDau);
        }

        // GET: GiaiDau/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GiaiDau giaiDau = db.GiaiDaus.Find(id);
            if (giaiDau == null)
            {
                return HttpNotFound();
            }
            return View(giaiDau);
        }

        // POST: GiaiDau/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GiaiDau giaiDau = db.GiaiDaus.Find(id);
            db.GiaiDaus.Remove(giaiDau);
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