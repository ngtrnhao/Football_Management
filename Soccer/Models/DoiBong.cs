using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Soccer.Models
{
    public partial class DoiBong
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DoiBong()
        {
            this.BangXepHangs = new HashSet<BangXepHang>();
            this.CauThus = new HashSet<CauThu>();
            this.ChuyenNhuongs = new HashSet<ChuyenNhuong>();
            this.ChuyenNhuongs1 = new HashSet<ChuyenNhuong>();
            this.DoiHinhTranDaus = new HashSet<DoiHinhTranDau>();
            this.HuanLuyenViens = new HashSet<HuanLuyenVien>();
            this.LichThiDaus = new HashSet<LichThiDau>();
            this.LichThiDaus1 = new HashSet<LichThiDau>();
            this.SuKienTranDaus = new HashSet<SuKienTranDau>();
            this.ThongKeTranDaus = new HashSet<ThongKeTranDau>();
            this.TranDaus = new HashSet<TranDau>();
            this.TranDaus1 = new HashSet<TranDau>();
        }

        public int MaDoi { get; set; }
        public string TenDoi { get; set; }
        public string ThanhPho { get; set; }
        public Nullable<int> MaSanNha { get; set; }
        public Nullable<int> NamThanhLap { get; set; }
        public Nullable<int> MaGiaiDau { get; set; }
        public string Logo { get; set; }

        [JsonIgnore]
        public virtual ICollection<BangXepHang> BangXepHangs { get; set; }
        [JsonIgnore]
        public virtual ICollection<CauThu> CauThus { get; set; }
        [JsonIgnore]
        public virtual ICollection<ChuyenNhuong> ChuyenNhuongs { get; set; }
        [JsonIgnore]
        public virtual ICollection<ChuyenNhuong> ChuyenNhuongs1 { get; set; }
        [JsonIgnore]
        public virtual GiaiDau GiaiDau { get; set; }
        [JsonIgnore]
        public virtual SanVanDong SanVanDong { get; set; }
        [JsonIgnore]
        public virtual ICollection<DoiHinhTranDau> DoiHinhTranDaus { get; set; }
        [JsonIgnore]
        public virtual ICollection<HuanLuyenVien> HuanLuyenViens { get; set; }
        [JsonIgnore]
        public virtual ICollection<LichThiDau> LichThiDaus { get; set; }
        [JsonIgnore]
        public virtual ICollection<LichThiDau> LichThiDaus1 { get; set; }
        [JsonIgnore]
        public virtual ICollection<SuKienTranDau> SuKienTranDaus { get; set; }
        [JsonIgnore]
        public virtual ICollection<ThongKeTranDau> ThongKeTranDaus { get; set; }
        [JsonIgnore]
        public virtual ICollection<TranDau> TranDaus { get; set; }
        [JsonIgnore]
        public virtual ICollection<TranDau> TranDaus1 { get; set; }
    }
}