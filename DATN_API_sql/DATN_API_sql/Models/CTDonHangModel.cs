using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATN_API_sql.Models
{
    public class CTDonHangModel
    {
        public string NguoiNhan { get; set; }

        public string HinhAnh { get; set; }
        public Nullable<double> GiaBan { get; set; }
        public string Ghichu { get; set; }
        public int? sodtkh { get; set; }

        public string TenSanPham { get; set; }
        public int IdSanPham { get; set; }
        public int? Trangthaimua { get; set; }
        public DateTime NgayDat { get; set; }
        public Nullable<int> SoLuong { get; set; }
        public string DiaChi { get; set; }
        public string ThoiGian { get; set; }
        public Nullable<double> Tongtienmua { get; set; }
        

    }
}