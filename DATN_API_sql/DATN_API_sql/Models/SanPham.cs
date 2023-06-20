namespace DATN_API_sql.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Xml.Serialization;
    using Newtonsoft.Json;

    [Table("Sach")]
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            ChiTietDonHangs = new HashSet<ChiTietDonHang>();
            ChiTietHDNs = new HashSet<ChiTietHDN>();
            DanhGias = new HashSet<DanhGia>();
        }

        public int SanPhamID { get; set; }

        [StringLength(250)]
        public string TENSP { get; set; }

        [StringLength(250)]
        public string IMAGE { get; set; }

        [StringLength(250)]
        public string IMAGE1 { get; set; }

        [StringLength(250)]
        public string IMAGE2 { get; set; }


        [StringLength(50)]
        public string TENTG { get; set; }

        public int? MALOAI { get; set; }

        [StringLength(250)]
        public string XUATXU { get; set; }

        public double? GIABAN { get; set; }

        public int? SOLUONG { get; set; }

        public string MOTA { get; set; }

        [StringLength(50)]
        public string TRANGTHAI { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CREATE_AT { get; set; }

        [Column(TypeName = "date")]
        public DateTime? UPDATE_AT { get; set; }

        public double? DIEMTB { get; set; }

        public int? SOLUONGTON { get; set; }

        public int? GIANHAP { get; set; }

        public int? LUOTDANHGIA { get; set; }
        [NotMapped]
        public string TENLOAI { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        [XmlIgnore]
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        [XmlIgnore]
        public virtual ICollection<ChiTietHDN> ChiTietHDNs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        [XmlIgnore]
        public virtual ICollection<DanhGia> DanhGias { get; set; }
        [JsonIgnore]
        [XmlIgnore]
        public virtual LoaiSanPham LoaiSanPham { get; set; }
    }
}
