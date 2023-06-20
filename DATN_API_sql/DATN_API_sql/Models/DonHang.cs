namespace DATN_API_sql.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Xml.Serialization;
    using Newtonsoft.Json;

    [Table("DonHang")]
    public partial class DonHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DonHang()
        {
            ChiTietDonHangs = new HashSet<ChiTietDonHang>();
        }

        public int DonHangID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NGAYDAT { get; set; }

        public int? TONGTIEN { get; set; }

        public int? KHACHHANG { get; set; }

        [StringLength(250)]
        public string GHICHU { get; set; }

        public int? NGUOITAO { get; set; }

        [StringLength(250)]
        public string DIACHI { get; set; }

        [StringLength(50)]
        public string SDT { get; set; }

        [StringLength(50)]
        public string TENKH { get; set; }

        public int? TRANGTHAI { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        [XmlIgnore]
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }
        [JsonIgnore]
        [XmlIgnore]
        public virtual KhachHang KhachHang1 { get; set; }
        [JsonIgnore]
        [XmlIgnore]
        public virtual NhanVien NhanVien { get; set; }
    }
}

public class DoanhThu
{
    public int? Thang { get; set; }
    public int? TongDoanhThuTheoThang { get; set; }
}

public class DoanhThuquy
{
    public int? Quy { get; set; }
    public int? TongDoanhThuTheoquy { get; set; }
}