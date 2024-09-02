using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Soccer.Models;
using System.Data.Entity.Infrastructure;


namespace Soccer.Controllers
{
    public class TranDauController : Controller
    {
        private QlyBongDaEntities db = new QlyBongDaEntities();

        // GET: TranDau
        public ActionResult Index(string searchString)
        {
            var tranDaus = db.TranDaus
                .Include(t => t.GiaiDau)
                .Include(t => t.DoiBong)
                .Include(t => t.DoiBong1)
                .Include(t => t.SanVanDong)
                .Include(t => t.TrongTai)
                .Include(t => t.DoiHinhTranDaus)
                .Include(t => t.SuKienTranDaus)
                .AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                tranDaus = tranDaus.Where(t =>
                    t.GiaiDau.TenGiaiDau.Contains(searchString) ||
                    t.DoiBong.TenDoi.Contains(searchString) ||
                    t.DoiBong1.TenDoi.Contains(searchString) ||
                    t.SanVanDong.TenSan.Contains(searchString) ||
                    t.TrangThai.Contains(searchString)
                );
            }

            return View(tranDaus.ToList());
        }

        // GET: TranDau/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TranDau tranDau = db.TranDaus.Find(id);
            if (tranDau == null)
            {
                return HttpNotFound();
            }
            return View(tranDau);
        }

        // GET: TranDau/Create
        public ActionResult Create()
        {
            ViewBag.MaGiaiDau = new SelectList(db.GiaiDaus, "MaGiaiDau", "TenGiaiDau");
            ViewBag.MaDoiNha = new SelectList(db.DoiBongs, "MaDoi", "TenDoi");
            ViewBag.MaDoiKhach = new SelectList(db.DoiBongs, "MaDoi", "TenDoi");
            ViewBag.MaSan = new SelectList(db.SanVanDongs, "MaSan", "TenSan");
            ViewBag.MaTrongTaiChinh = new SelectList(db.TrongTais, "MaTrongTai", "Ho");
            ViewBag.TrangThai = new SelectList(new List<SelectListItem>
{
                    new SelectListItem { Value = "Chưa diễn ra", Text = "Chưa diễn ra" },
                    new SelectListItem { Value = "Đang diễn ra", Text = "Đang diễn ra" },
                    new SelectListItem { Value = "Đã kết thúc", Text = "Đã kết thúc" }
            }, "Value", "Text");
            return View();
        }

        // POST: TranDau/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaGiaiDau,MaDoiNha,MaDoiKhach,MaSan,ThoiGianDuKien,TrangThai,MaTrongTaiChinh,TiSoDoiNha,TiSoDoiKhach")] TranDau tranDau)
        {
            if (ModelState.IsValid)
            {
                db.TranDaus.Add(tranDau);
                db.SaveChanges();
                LichThiDauController lichThiDauController = new LichThiDauController();
                lichThiDauController.CapNhatLichThiDau(tranDau.MaTran);
                return RedirectToAction("Index");
            }

            ViewBag.MaGiaiDau = new SelectList(db.GiaiDaus, "MaGiaiDau", "TenGiaiDau", tranDau.MaGiaiDau);
            ViewBag.MaDoiNha = new SelectList(db.DoiBongs, "MaDoi", "TenDoi", tranDau.MaDoiNha);
            ViewBag.MaDoiKhach = new SelectList(db.DoiBongs, "MaDoi", "TenDoi", tranDau.MaDoiKhach);
            ViewBag.MaSan = new SelectList(db.SanVanDongs, "MaSan", "TenSan", tranDau.MaSan);
            ViewBag.MaTrongTaiChinh = new SelectList(db.TrongTais, "MaTrongTai", "Ho", tranDau.MaTrongTaiChinh);
            return View(tranDau);
        }

        // GET: TranDau/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TranDau tranDau = db.TranDaus.Find(id);
            if (tranDau == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaGiaiDau = new SelectList(db.GiaiDaus, "MaGiaiDau", "TenGiaiDau", tranDau.MaGiaiDau);
            ViewBag.MaDoiNha = new SelectList(db.DoiBongs, "MaDoi", "TenDoi", tranDau.MaDoiNha);
            ViewBag.MaDoiKhach = new SelectList(db.DoiBongs, "MaDoi", "TenDoi", tranDau.MaDoiKhach);
            ViewBag.MaSan = new SelectList(db.SanVanDongs, "MaSan", "TenSan", tranDau.MaSan);
            ViewBag.MaTrongTaiChinh = new SelectList(db.TrongTais, "MaTrongTai", "Ho", tranDau.MaTrongTaiChinh);
            ViewBag.TrangThai = new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Value = "Chưa diễn ra", Text = "Chưa diễn ra" },
                new SelectListItem { Value = "Đang diễn ra", Text = "Đang diễn ra" },
                new SelectListItem { Value = "Đã kết thúc", Text = "Đã kết thúc" }
            }, "Value", "Text");
            return View(tranDau);
        }

        // POST: TranDau/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id)
        {
            var tranDau = db.TranDaus.Find(id);
            if (tranDau == null)
            {
                return HttpNotFound();
            }

            if (TryUpdateModel(tranDau, "",
                new string[] { "MaGiaiDau", "MaDoiNha", "MaDoiKhach", "MaSan", "ThoiGianDuKien", "TrangThai", "MaTrongTaiChinh", "TiSoDoiNha", "TiSoDoiKhach" }))
            {
                try
                {
                    // Lưu trạng thái cũ trước khi cập nhật
                    string trangThaiCu = db.Entry(tranDau).OriginalValues["TrangThai"].ToString();

                    db.Entry(tranDau).State = EntityState.Modified;

                    // Nếu trạng thái mới là "Đang diễn ra" và khác với trạng thái cũ
                    if (tranDau.TrangThai == "Đang diễn ra" && trangThaiCu != "Đang diễn ra")
                    {
                        // Cập nhật LichThiDau
                        var lichThiDau = db.LichThiDaus.FirstOrDefault(l => l.MaTran == id);
                        if (lichThiDau != null)
                        {
                            lichThiDau.TrangThai = "Đang diễn ra";
                            db.Entry(lichThiDau).State = EntityState.Modified;
                        }
                        var bangXepHangController = new BangXepHangController(db);
                        bangXepHangController.CapNhatBangXepHang(id);
                    }

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var entry = ex.Entries.Single();
                    var clientValues = (TranDau)entry.Entity;
                    var databaseEntry = entry.GetDatabaseValues();
                    if (databaseEntry == null)
                    {
                        ModelState.AddModelError(string.Empty, "Unable to save changes. The TranDau was deleted by another user.");
                    }
                    else
                    {
                        var databaseValues = (TranDau)databaseEntry.ToObject();
                        ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                            + "was modified by another user after you got the original value. The "
                            + "edit operation was canceled and the current values in the database "
                            + "have been displayed. If you still want to edit this record, click "
                            + "the Save button again.");
                        entry.OriginalValues.SetValues(databaseValues);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }

                        ViewBag.MaGiaiDau = new SelectList(db.GiaiDaus, "MaGiaiDau", "TenGiaiDau", tranDau.MaGiaiDau);
                        ViewBag.MaDoiNha = new SelectList(db.DoiBongs, "MaDoi", "TenDoi", tranDau.MaDoiNha);
                        ViewBag.MaDoiKhach = new SelectList(db.DoiBongs, "MaDoi", "TenDoi", tranDau.MaDoiKhach);
                        ViewBag.MaSan = new SelectList(db.SanVanDongs, "MaSan", "TenSan", tranDau.MaSan);
                        ViewBag.MaTrongTaiChinh = new SelectList(db.TrongTais, "MaTrongTai", "Ho", tranDau.MaTrongTaiChinh);
                        ViewBag.TrangThai = new SelectList(new List<SelectListItem>
                        {
                            new SelectListItem { Value = "Chưa diễn ra", Text = "Chưa diễn ra" },
                            new SelectListItem { Value = "Đang diễn ra", Text = "Đang diễn ra" },
                            new SelectListItem { Value = "Đã kết thúc", Text = "Đã kết thúc" }
                        }, "Value", "Text", tranDau.TrangThai);

                        return View(tranDau);
                    }
        // GET: TranDau/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TranDau tranDau = db.TranDaus.Find(id);
            if (tranDau == null)
            {
                return HttpNotFound();
            }
            return View(tranDau);
        }

        // POST: TranDau/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TranDau tranDau = db.TranDaus.Find(id);
            db.TranDaus.Remove(tranDau);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: TranDau/DangDienRa
        public ActionResult DangDienRa()
        {
            var tranDauDangDienRa = db.TranDaus
                .Where(t => t.TrangThai == "Đang diễn ra")
                .Include(t => t.GiaiDau)
                .Include(t => t.DoiBong)
                .Include(t => t.DoiBong1)
                .Include(t => t.SanVanDong);
            return View(tranDauDangDienRa.ToList());
        }

        // GET: TranDau/ChinhSuaDangDienRa/5
        public ActionResult ChinhSuaDangDienRa(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TranDau tranDau = db.TranDaus.Find(id);
            if (tranDau == null)
            {
                return HttpNotFound();
            }
            if (tranDau.TrangThai != "Đang diễn ra")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Trận đấu không đang diễn ra");
            }
            return View(tranDau);
        }

        // POST: TranDau/ChinhSuaDangDienRa/5
        // POST: TranDau/ChinhSuaDangDienRa/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChinhSuaDangDienRa(int id, int tiSoDoiNha, int tiSoDoiKhach)
        {
            TranDau tranDau = db.TranDaus.Find(id);
            if (tranDau == null || tranDau.TrangThai != "Đang diễn ra")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Trận đấu không đang diễn ra");
            }

            tranDau.TiSoDoiNha = tiSoDoiNha;
            tranDau.TiSoDoiKhach = tiSoDoiKhach;

            if (ModelState.IsValid)
            {
                db.Entry(tranDau).State = EntityState.Modified;

                // Cập nhật LichThiDau
                var lichThiDau = db.LichThiDaus.FirstOrDefault(l => l.MaTran == id);
                if (lichThiDau != null)
                {
                    lichThiDau.TiSoDoiNha = tiSoDoiNha;
                    lichThiDau.TiSoDoiKhach = tiSoDoiKhach;
                    db.Entry(lichThiDau).State = EntityState.Modified;
                }
                BangXepHangController bangXepHangController = new BangXepHangController(db);
                bangXepHangController.CapNhatBangXepHang(id);
                db.SaveChanges();
                return RedirectToAction("DangDienRa");
            }
            return View(tranDau);
        }

        public ActionResult DoiHinh(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var tranDau = db.TranDaus.Find(id);
            if (tranDau == null)
            {
                return HttpNotFound();
            }

            var doiHinhNha = db.DoiHinhTranDaus
                .Where(d => d.MaTran == id && d.MaDoi == tranDau.MaDoiNha)
                .Include(d => d.CauThu)
                .OrderByDescending(d => d.LaDoi11Chinh)
                .ThenBy(d => d.ViTri)
                .ToList();

            var doiHinhKhach = db.DoiHinhTranDaus
                .Where(d => d.MaTran == id && d.MaDoi == tranDau.MaDoiKhach)
                .Include(d => d.CauThu)
                .OrderByDescending(d => d.LaDoi11Chinh)
                .ThenBy(d => d.ViTri)
                .ToList();

            ViewBag.DoiNha = db.DoiBongs.Find(tranDau.MaDoiNha).TenDoi;
            ViewBag.DoiKhach = db.DoiBongs.Find(tranDau.MaDoiKhach).TenDoi;
            ViewBag.MaTran = id;

            var viewModel = new Tuple<List<DoiHinhTranDau>, List<DoiHinhTranDau>>(doiHinhNha, doiHinhKhach);
            return View(viewModel);
        }
        public ActionResult TaoDoiHinh(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TranDau tranDau = db.TranDaus.Find(id);
            if (tranDau == null)
            {
                return HttpNotFound();
            }
            ViewBag.CauThuDoiNha = db.CauThus
                .Where(c => c.MaDoi == tranDau.MaDoiNha)
                .Select(c => new
                {
                    c.MaCauThu,
                    c.Ho,
                    c.Ten,
                    c.ViTri,
                    c.SoAo
                })
                .ToList();
            ViewBag.CauThuDoiKhach = db.CauThus
                 .Where(c => c.MaDoi == tranDau.MaDoiKhach)
                 .Select(c => new
                 {
                     c.MaCauThu,
                     c.Ho,
                     c.Ten,
                     c.ViTri,
                     c.SoAo
                 })
                 .ToList();
            return View(tranDau);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TaoDoiHinh(int id, List<int> doiHinhChinhNha, List<int> duBiNha, List<int> doiHinhChinhKhach, List<int> duBiKhach)
        {
            var tranDau = db.TranDaus.Find(id);
            if (tranDau == null)
            {
                return HttpNotFound();
            }

            if (doiHinhChinhNha.Count != 11 || doiHinhChinhKhach.Count != 11 || duBiNha.Count > 7 || duBiKhach.Count > 7)
            {
                ModelState.AddModelError("", "Đội hình không hợp lệ. Vui lòng kiểm tra lại.");
                ViewBag.CauThuDoiNha = db.CauThus.Where(c => c.MaDoi == tranDau.MaDoiNha).ToList();
                ViewBag.CauThuDoiKhach = db.CauThus.Where(c => c.MaDoi == tranDau.MaDoiKhach).ToList();
                return View(tranDau);
            }

            var doiHinhCu = db.DoiHinhTranDaus.Where(d => d.MaTran == id);
            db.DoiHinhTranDaus.RemoveRange(doiHinhCu);

            AddDoiHinh(id, tranDau.MaDoiNha, doiHinhChinhNha, true);
            AddDoiHinh(id, tranDau.MaDoiNha, duBiNha, false);

            AddDoiHinh(id, tranDau.MaDoiKhach, doiHinhChinhKhach, true);
            AddDoiHinh(id, tranDau.MaDoiKhach, duBiKhach, false);

            db.SaveChanges();
            return RedirectToAction("DoiHinh", new { id = id });
        }

        private void AddDoiHinh(int maTran, int? maDoi, List<int> danhSachCauThu, bool laDoi11Chinh)
        {
            if (maDoi.HasValue)
            {
                foreach (var cauThuId in danhSachCauThu)
                {
                    db.DoiHinhTranDaus.Add(new DoiHinhTranDau
                    {
                        MaTran = maTran,
                        MaDoi = maDoi.Value,
                        MaCauThu = cauThuId,
                        LaDoi11Chinh = laDoi11Chinh
                    });
                }
            }
        }
        // GET: TranDau/ThemSuKien/5
        public ActionResult ThemSuKien(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TranDau tranDau = db.TranDaus.Find(id);
            if (tranDau == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDoi = new SelectList(new[] { tranDau.DoiBong, tranDau.DoiBong1 }, "MaDoi", "TenDoi");
            ViewBag.MaCauThu = new SelectList(db.CauThus.Where(c => c.MaDoi == tranDau.MaDoiNha || c.MaDoi == tranDau.MaDoiKhach), "MaCauThu", "Ten");
            return View();
        }

        // POST: TranDau/ThemSuKien/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemSuKien([Bind(Include = "MaSuKien,MaTran,MaDoi,MaCauThu,LoaiSuKien,PhutThoiGian,MoTa")] SuKienTranDau suKienTranDau)
        {
            if (ModelState.IsValid)
            {
                db.SuKienTranDaus.Add(suKienTranDau);
                db.SaveChanges();

                // Cập nhật thống kê cầu thủ
                var cauThu = db.CauThus.Find(suKienTranDau.MaCauThu);
                if (cauThu != null)
                {
                    switch (suKienTranDau.LoaiSuKien)
                    {
                        case "TheVang":
                            cauThu.SoTheVang++;
                            break;
                        case "TheDo":
                            cauThu.SoTheDo++;
                            break;
                        case "GhiBan":
                            cauThu.SoBanThang++;
                            break;
                        case "KienTao":
                            cauThu.SoKienTao++;
                            break;
                    }
                    db.SaveChanges();
                }

                return RedirectToAction("Details", new { id = suKienTranDau.MaTran });
            }

            ViewBag.MaDoi = new SelectList(db.DoiBongs, "MaDoi", "TenDoi", suKienTranDau.MaDoi);
            ViewBag.MaCauThu = new SelectList(db.CauThus, "MaCauThu", "HoTen", suKienTranDau.MaCauThu);
            return View(suKienTranDau);
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
