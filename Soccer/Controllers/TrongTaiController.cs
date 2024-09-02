using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Soccer.Models;

namespace Soccer.Controllers
{
    public class TrongTaiController : Controller
    {
        private QlyBongDaEntities db = new QlyBongDaEntities();

        public ActionResult Index()
        {
            return View(db.TrongTais.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrongTai trongTai = db.TrongTais.Find(id);
            if (trongTai == null)
            {
                return HttpNotFound();
            }
            return View(trongTai);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Ho,Ten,NgaySinh,QuocTich,SoNamKinhNghiem")] TrongTai trongTai)
        {
            if (ModelState.IsValid)
            {
                db.TrongTais.Add(trongTai);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(trongTai);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrongTai trongTai = db.TrongTais.Find(id);
            if (trongTai == null)
            {
                return HttpNotFound();
            }
            return View(trongTai);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaTrongTai,Ho,Ten,NgaySinh,QuocTich,SoNamKinhNghiem")] TrongTai trongTai)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trongTai).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(trongTai);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrongTai trongTai = db.TrongTais.Find(id);
            if (trongTai == null)
            {
                return HttpNotFound();
            }
            return View(trongTai);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TrongTai trongTai = db.TrongTais.Find(id);
            db.TrongTais.Remove(trongTai);
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