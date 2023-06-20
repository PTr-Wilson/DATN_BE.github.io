namespace DATN_API_sql.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Xml.Serialization;
    using Newtonsoft.Json;

    [Table("ChiTietDonHang")]
    public partial class ChiTietDonHang
    {
        [Key]
        public int CTDonHangID { get; set; }

        public int MAHDX { get; set; }

        public int? MASP { get; set; }

        public int? SOLUONG { get; set; }

        public double? GIABAN { get; set; }

        public double? THANHTIEN { get; set; }

        public int? TRANGTHAI { get; set; }
        [JsonIgnore]
        [XmlIgnore]
        public virtual DonHang DonHang { get; set; }
        [JsonIgnore]
        [XmlIgnore]
        public virtual SanPham SanPham { get; set; }
    }
}
