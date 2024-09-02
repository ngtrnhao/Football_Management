using System;
using System.Linq;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using Soccer.Models;

namespace Soccer.Controllers
{
    public class SanVanDongController : Controller
    {
        private QlyBongDaEntities db = new QlyBongDaEntities();

        // GET: SanVanDong
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            IQueryable<SanVanDong> sanVanDongs = db.SanVanDongs;

            if (!string.IsNullOrEmpty(searchString))
            {
                sanVanDongs = sanVanDongs.Where(s => s.TenSan.Contains(searchString) || s.ThanhPho.Contains(searchString));
            }

            sanVanDongs = sanVanDongs.OrderBy(s => s.TenSan);

            int totalItems = sanVanDongs.Count();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var pagedSanVanDongs = sanVanDongs.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.PageSize = pageSize;
            ViewBag.SearchString = searchString;

            return View(pagedSanVanDongs);
        }

        // GET: SanVanDong/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanVanDong sanVanDong = db.SanVanDongs.Find(id);
            if (sanVanDong == null)
            {
                return HttpNotFound();
            }
            return View(sanVanDong);
        }

        // GET: SanVanDong/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SanVanDong/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSan,TenSan,ThanhPho,SucChua,NamXayDung,DiaChi,MoTa")] SanVanDong sanVanDong)
        {
            if (ModelState.IsValid)
            {
                db.SanVanDongs.Add(sanVanDong);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sanVanDong);
        }

        // GET: SanVanDong/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanVanDong sanVanDong = db.SanVanDongs.Find(id);
            if (sanVanDong == null)
            {
                return HttpNotFound();
            }
            return View(sanVanDong);
        }

        // POST: SanVanDong/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSan,TenSan,ThanhPho,SucChua,NamXayDung,DiaChi,MoTa")] SanVanDong sanVanDong)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanVanDong).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sanVanDong);
        }

        // GET: SanVanDong/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanVanDong sanVanDong = db.SanVanDongs.Find(id);
            if (sanVanDong == null)
            {
                return HttpNotFound();
            }
            return View(sanVanDong);
        }

        // POST: SanVanDong/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SanVanDong sanVanDong = db.SanVanDongs.Find(id);
            db.SanVanDongs.Remove(sanVanDong);
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