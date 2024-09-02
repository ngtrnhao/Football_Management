using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Soccer.Models
{
    public partial class CauThu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CauThu()
        {
            this.ChanThuongs = new HashSet<ChanThuong>();
            this.ChuyenNhuongs = new HashSet<ChuyenNhuong>();
            this.DoiHinhTranDaus = new HashSet<DoiHinhTranDau>();
            this.SuKienTranDaus = new HashSet<SuKienTranDau>();
            this.SuKienTranDaus1 = new HashSet<SuKienTranDau>();
            this.SuKienTranDaus2 = new HashSet<SuKienTranDau>();
            this.ThongKeTranDaus = new HashSet<ThongKeTranDau>();
        }

        public int MaCauThu { get; set; }
        public string Ho { get; set; }
        public string Ten { get; set; }
        public Nullable<System.DateTime> NgaySinh { get; set; }
        public string QuocTich { get; set; }
        public string ViTri { get; set; }
        public Nullable<int> SoAo { get; set; }
        public Nullable<int> MaDoi { get; set; }
        public string HinhAnh { get; set; }
        public Nullable<int> SoBanThang { get; set; }
        public Nullable<int> SoKienTao { get; set; }
        public Nullable<int> SoTheVang { get; set; }
        public Nullable<int> SoTheDo { get; set; }

        [JsonIgnore]
        public virtual DoiBong DoiBong { get; set; }
        [JsonIgnore]
        public virtual ICollection<ChanThuong> ChanThuongs { get; set; }
        [JsonIgnore]
        public virtual ICollection<ChuyenNhuong> ChuyenNhuongs { get; set; }
        [JsonIgnore]
        public virtual ICollection<DoiHinhTranDau> DoiHinhTranDaus { get; set; }
        [JsonIgnore]
        public virtual ICollection<SuKienTranDau> SuKienTranDaus { get; set; }
        [JsonIgnore]
        public virtual ICollection<SuKienTranDau> SuKienTranDaus1 { get; set; }
        [JsonIgnore]
        public virtual ICollection<SuKienTranDau> SuKienTranDaus2 { get; set; }
        [JsonIgnore]
        public virtual ICollection<ThongKeTranDau> ThongKeTranDaus { get; set; }
    }
}