namespace DATN_API_sql.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Xml.Serialization;
    using Newtonsoft.Json;

    [Table("ChiTietHDN")]
    public partial class ChiTietHDN
    {
        [Key]
        public int CTHdnID { get; set; }

        public int HdnID { get; set; }

        public int? MASP { get; set; }

        public int? SOLUONG { get; set; }

        public double? GIANHAP { get; set; }

        public double? THANHTIEN { get; set; }

        public int? TRANGTHAI { get; set; }
        [JsonIgnore]
        [XmlIgnore]
        public virtual HoaDonNhap HoaDonNhap { get; set; }
        [JsonIgnore]
        [XmlIgnore]
        public virtual SanPham SanPham { get; set; }
    }
}
