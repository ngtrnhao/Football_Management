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
    public class CauThuController : Controller
    {
        private QlyBongDaEntities db = new QlyBongDaEntities();

        public ActionResult Index()
        {
            var cauThus = db.CauThus.Include(c => c.DoiBong);
            return View(cauThus.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CauThu cauThu = db.CauThus.Find(id);
            if (cauThu == null)
            {
                return HttpNotFound();
            }
            return View(cauThu);
        }

        public ActionResult Create()
        {
            ViewBag.MaDoi = new SelectList(db.DoiBongs, "MaDoi", "TenDoi");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Ho,Ten,NgaySinh,QuocTich,ViTri,SoAo,MaDoi")] CauThu cauThu, HttpPostedFileBase hinhAnh)
        {
            if (ModelState.IsValid)
            {
                if (hinhAnh != null && hinhAnh.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(hinhAnh.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images/CauThu"), fileName);
                    hinhAnh.SaveAs(path);
                    cauThu.HinhAnh = "/Images/CauThu/" + fileName;
                }

                db.CauThus.Add(cauThu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaDoi = new SelectList(db.DoiBongs, "MaDoi", "TenDoi", cauThu.MaDoi);
            return View(cauThu);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CauThu cauThu = db.CauThus.Find(id);
            if (cauThu == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDoi = new SelectList(db.DoiBongs, "MaDoi", "TenDoi", cauThu.MaDoi);
            return View(cauThu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaCauThu,Ho,Ten,NgaySinh,QuocTich,ViTri,SoAo,MaDoi,HinhAnh,SoBanThang,SoKienTao,SoTheVang,SoTheDo")] CauThu cauThu, HttpPostedFileBase hinhAnh)
        {
            if (ModelState.IsValid)
            {
                var existingCauThu = db.CauThus.Find(cauThu.MaCauThu);
                if (existingCauThu == null)
                {
                    return HttpNotFound();
                }

                // Cập nhật các trường
                existingCauThu.Ho = cauThu.Ho;
                existingCauThu.Ten = cauThu.Ten;
                existingCauThu.NgaySinh = cauThu.NgaySinh;
                existingCauThu.QuocTich = cauThu.QuocTich;
                existingCauThu.ViTri = cauThu.ViTri;
                existingCauThu.SoAo = cauThu.SoAo;
                existingCauThu.MaDoi = cauThu.MaDoi;
                existingCauThu.SoBanThang = cauThu.SoBanThang;
                existingCauThu.SoKienTao = cauThu.SoKienTao;
                existingCauThu.SoTheVang = cauThu.SoTheVang;
                existingCauThu.SoTheDo = cauThu.SoTheDo;

                // Xử lý upload ảnh mới
                if (hinhAnh != null && hinhAnh.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(hinhAnh.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images/CauThu"), fileName);
                    hinhAnh.SaveAs(path);
                    existingCauThu.HinhAnh = "/Images/CauThu/" + fileName;
                }
                // Nếu không có ảnh mới, giữ nguyên ảnh cũ
                else
                {
                    existingCauThu.HinhAnh = cauThu.HinhAnh;
                }

                db.Entry(existingCauThu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaDoi = new SelectList(db.DoiBongs, "MaDoi", "TenDoi", cauThu.MaDoi);
            return View(cauThu);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CauThu cauThu = db.CauThus.Find(id);
            if (cauThu == null)
            {
                return HttpNotFound();
            }
            return View(cauThu);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CauThu cauThu = db.CauThus.Find(id);
            db.CauThus.Remove(cauThu);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // New action to display player statistics
        public ActionResult Statistics(int id)
        {
            var cauThu = db.CauThus.Find(id);
            if (cauThu == null)
            {
                return HttpNotFound();
            }
            return View(cauThu);
        }
        // GET: CauThu/ThongKe/5
        public ActionResult ThongKe(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CauThu cauThu = db.CauThus.Find(id);
            if (cauThu == null)
            {
                return HttpNotFound();
            }
            return View(cauThu);
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