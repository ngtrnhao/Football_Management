using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Soccer.Models;

namespace Soccer.Controllers
{
    public class BangXepHangController : Controller
    {
        private QlyBongDaEntities db;

        public BangXepHangController()
        {
            db = new QlyBongDaEntities();
        }

        public BangXepHangController(QlyBongDaEntities context)
        {
            db = context;
        }

        // GET: BangXepHang
        public ActionResult Index(int? giaiDauId)
        {
            IQueryable<BangXepHang> bangXepHangQuery = db.BangXepHangs;
            if (giaiDauId.HasValue)
            {
                bangXepHangQuery = bangXepHangQuery.Where(b => b.MaGiaiDau == giaiDauId.Value);
            }
            var bangXepHang = bangXepHangQuery.OrderByDescending(b => b.Diem)
                                             .ThenByDescending(b => b.HieuSo)
                                             .ThenByDescending(b => b.BanThang)
                                             .ToList();
            return View(bangXepHang);
        }

        public void CapNhatBangXepHang(int maTran)
        {
            var tranDau = db.TranDaus.Find(maTran);
            if (tranDau == null)
            {
                return;
            }
            // Cập nhật bảng xếp hạng cho đội nhà
            CapNhatBangXepHangMoiDoi(tranDau.MaDoiNha ?? 0, tranDau.TiSoDoiNha, tranDau.TiSoDoiKhach, tranDau.MaGiaiDau ?? 0);
            // Cập nhật bảng xếp hạng cho đội khách
            CapNhatBangXepHangMoiDoi(tranDau.MaDoiKhach ?? 0, tranDau.TiSoDoiKhach, tranDau.TiSoDoiNha, tranDau.MaGiaiDau ?? 0);
        }

        private void CapNhatBangXepHangMoiDoi(int maDoi, int? tiSoDoiNay, int? tiSoDoiKia, int maGiaiDau)
        {
            var bangXepHang = db.BangXepHangs.FirstOrDefault(b => b.MaGiaiDau == maGiaiDau && b.MaDoi == maDoi);
            if (bangXepHang == null)
            {
                // Tạo mới bản ghi bảng xếp hạng nếu chưa có
                var giaiDau = db.GiaiDaus.Find(maGiaiDau);
                bangXepHang = new BangXepHang
                {
                    MaGiaiDau = maGiaiDau,
                    MaDoi = maDoi,
                    MuaGiai = giaiDau?.MuaGiai,
                    ViTri = 0,
                    SoTranDau = 0,
                    SoTranThang = 0,
                    SoTranHoa = 0,
                    SoTranThua = 0,
                    BanThang = 0,
                    BanThua = 0,
                    HieuSo = 0,
                    Diem = 0
                };
                db.BangXepHangs.Add(bangXepHang);
            }

            // Cập nhật thông số bảng xếp hạng
            bangXepHang.SoTranDau++;
            if (tiSoDoiNay > tiSoDoiKia)
            {
                bangXepHang.SoTranThang++;
                bangXepHang.Diem += 3;
            }
            else if (tiSoDoiNay < tiSoDoiKia)
            {
                bangXepHang.SoTranThua++;
            }
            else
            {
                bangXepHang.SoTranHoa++;
                bangXepHang.Diem += 1;
            }

            bangXepHang.BanThang += tiSoDoiNay ?? 0;
            bangXepHang.BanThua += tiSoDoiKia ?? 0;
            bangXepHang.HieuSo = bangXepHang.BanThang - bangXepHang.BanThua;

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