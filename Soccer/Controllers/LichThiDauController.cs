using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Soccer.Models;

namespace Soccer.Controllers
{
    public class LichThiDauController : Controller
    {
        private QlyBongDaEntities db = new QlyBongDaEntities();

        // GET: LichThiDau
        public ActionResult Index()
        {
            var lichThiDaus = db.LichThiDaus
                .Include(l => l.GiaiDau)
                .Include(l => l.DoiBong)
                .Include(l => l.DoiBong1)
                .Include(l => l.SanVanDong)
                .Include(l => l.TranDau)
                .OrderBy(l => l.ThoiGianDuKien)
                .ToList();

            var groupedMatches = lichThiDaus
                .Where(l => l.ThoiGianDuKien.HasValue)
                .GroupBy(l => l.ThoiGianDuKien.Value.Date)
                .OrderBy(g => g.Key);

            return View(groupedMatches);
        }
        // GET: LichThiDau/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LichThiDau lichThiDau = db.LichThiDaus.Find(id);
            if (lichThiDau == null)
            {
                return HttpNotFound();
            }
            return View(lichThiDau);
        }

        // GET: LichThiDau/Create
        public ActionResult Create()
        {
            ViewBag.MaTran = new SelectList(db.TranDaus, "MaTran", "MaTran");
            ViewBag.MaGiaiDau = new SelectList(db.GiaiDaus, "MaGiaiDau", "TenGiaiDau");
            ViewBag.MaDoiNha = new SelectList(db.DoiBongs, "MaDoi", "TenDoi");
            ViewBag.MaDoiKhach = new SelectList(db.DoiBongs, "MaDoi", "TenDoi");
            ViewBag.MaSan = new SelectList(db.SanVanDongs, "MaSan", "TenSan");
            return View();
        }

        // POST: LichThiDau/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaTran,MaGiaiDau,MaDoiNha,MaDoiKhach,MaSan,ThoiGianDuKien,TrangThai,TiSoDoiNha,TiSoDoiKhach")] LichThiDau lichThiDau)
        {
            if (ModelState.IsValid)
            {
                db.LichThiDaus.Add(lichThiDau);
                db.SaveChanges();

                // Cập nhật hoặc tạo TranDau tương ứng
                CapNhatTranDau(lichThiDau);

                return RedirectToAction("Index");
            }

            ViewBag.MaTran = new SelectList(db.TranDaus, "MaTran", "MaTran", lichThiDau.MaTran);
            ViewBag.MaGiaiDau = new SelectList(db.GiaiDaus, "MaGiaiDau", "TenGiaiDau", lichThiDau.MaGiaiDau);
            ViewBag.MaDoiNha = new SelectList(db.DoiBongs, "MaDoi", "TenDoi", lichThiDau.MaDoiNha);
            ViewBag.MaDoiKhach = new SelectList(db.DoiBongs, "MaDoi", "TenDoi", lichThiDau.MaDoiKhach);
            ViewBag.MaSan = new SelectList(db.SanVanDongs, "MaSan", "TenSan", lichThiDau.MaSan);
            return View(lichThiDau);
        }

        // GET: LichThiDau/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LichThiDau lichThiDau = db.LichThiDaus.Find(id);
            if (lichThiDau == null)
            {
                return HttpNotFound();
            }

            ViewBag.MaTran = new SelectList(db.TranDaus, "MaTran", "MaTran", lichThiDau.MaTran);
            ViewBag.MaGiaiDau = new SelectList(db.GiaiDaus, "MaGiaiDau", "TenGiaiDau", lichThiDau.MaGiaiDau);
            ViewBag.MaDoiNha = new SelectList(db.DoiBongs, "MaDoi", "TenDoi", lichThiDau.MaDoiNha);
            ViewBag.MaDoiKhach = new SelectList(db.DoiBongs, "MaDoi", "TenDoi", lichThiDau.MaDoiKhach);
            ViewBag.MaSan = new SelectList(db.SanVanDongs, "MaSan", "TenSan", lichThiDau.MaSan);
            return View(lichThiDau);
        }

        // POST: LichThiDau/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaLich,MaTran,MaGiaiDau,MaDoiNha,MaDoiKhach,MaSan,ThoiGianDuKien,TrangThai,TiSoDoiNha,TiSoDoiKhach")] LichThiDau lichThiDau)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lichThiDau).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaTran = new SelectList(db.TranDaus, "MaTran", "MaTran", lichThiDau.MaTran);
            ViewBag.MaGiaiDau = new SelectList(db.GiaiDaus, "MaGiaiDau", "TenGiaiDau", lichThiDau.MaGiaiDau);
            ViewBag.MaDoiNha = new SelectList(db.DoiBongs, "MaDoi", "TenDoi", lichThiDau.MaDoiNha);
            ViewBag.MaDoiKhach = new SelectList(db.DoiBongs, "MaDoi", "TenDoi", lichThiDau.MaDoiKhach);
            ViewBag.MaSan = new SelectList(db.SanVanDongs, "MaSan", "TenSan", lichThiDau.MaSan);
            return View(lichThiDau);
        }

        // GET: LichThiDau/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LichThiDau lichThiDau = db.LichThiDaus.Find(id);
            if (lichThiDau == null)
            {
                return HttpNotFound();
            }
            return View(lichThiDau);
        }

        // POST: LichThiDau/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LichThiDau lichThiDau = db.LichThiDaus.Find(id);
            db.LichThiDaus.Remove(lichThiDau);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        private void CapNhatTranDau(LichThiDau lichThiDau)
        {
            var tranDau = db.TranDaus.Find(lichThiDau.MaTran);
            if (tranDau == null)
            {
                tranDau = new TranDau
                {
                    MaTran = (int)lichThiDau.MaTran,
                    MaGiaiDau = lichThiDau.MaGiaiDau,
                    MaDoiNha = lichThiDau.MaDoiNha,
                    MaDoiKhach = lichThiDau.MaDoiKhach,
                    MaSan = lichThiDau.MaSan,
                    ThoiGianDuKien = lichThiDau.ThoiGianDuKien,
                    TrangThai = lichThiDau.TrangThai,
                    TiSoDoiNha = lichThiDau.TiSoDoiNha,
                    TiSoDoiKhach = lichThiDau.TiSoDoiKhach
                };
                db.TranDaus.Add(tranDau);
            }
            else
            {
                tranDau.MaGiaiDau = lichThiDau.MaGiaiDau;
                tranDau.MaDoiNha = lichThiDau.MaDoiNha;
                tranDau.MaDoiKhach = lichThiDau.MaDoiKhach;
                tranDau.MaSan = lichThiDau.MaSan;
                tranDau.ThoiGianDuKien = lichThiDau.ThoiGianDuKien;
                tranDau.TrangThai = lichThiDau.TrangThai;
                tranDau.TiSoDoiNha = lichThiDau.TiSoDoiNha;
                tranDau.TiSoDoiKhach = lichThiDau.TiSoDoiKhach;
            }
            db.SaveChanges();
        }
        public void CapNhatLichThiDau(int maTran)
        {
            TranDau tranDau = db.TranDaus.Find(maTran);
            if (tranDau == null)
            {
                throw new Exception("Trận đấu không tồn tại.");
            }

            var lichThiDau = db.LichThiDaus.FirstOrDefault(l => l.MaTran == maTran);
            if (lichThiDau == null)
            {
                lichThiDau = new LichThiDau
                {
                    MaTran = tranDau.MaTran,
                    MaGiaiDau = tranDau.MaGiaiDau ?? 0,
                    MaDoiNha = tranDau.MaDoiNha ?? 0,
                    MaDoiKhach = tranDau.MaDoiKhach ?? 0,
                    MaSan = tranDau.MaSan ?? 0,
                    ThoiGianDuKien = tranDau.ThoiGianDuKien,
                    TrangThai = tranDau.TrangThai,
                    TiSoDoiNha = tranDau.TiSoDoiNha ?? 0,
                    TiSoDoiKhach = tranDau.TiSoDoiKhach ?? 0
                };
                db.LichThiDaus.Add(lichThiDau);
            }
            else
            {
                lichThiDau.MaGiaiDau = tranDau.MaGiaiDau ?? lichThiDau.MaGiaiDau;
                lichThiDau.MaDoiNha = tranDau.MaDoiNha ?? lichThiDau.MaDoiNha;
                lichThiDau.MaDoiKhach = tranDau.MaDoiKhach ?? lichThiDau.MaDoiKhach;
                lichThiDau.MaSan = tranDau.MaSan ?? lichThiDau.MaSan;
                lichThiDau.ThoiGianDuKien = tranDau.ThoiGianDuKien;
                lichThiDau.TrangThai = tranDau.TrangThai;
                lichThiDau.TiSoDoiNha = tranDau.TiSoDoiNha ?? lichThiDau.TiSoDoiNha;
                lichThiDau.TiSoDoiKhach = tranDau.TiSoDoiKhach ?? lichThiDau.TiSoDoiKhach;
            }
            db.SaveChanges();
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
