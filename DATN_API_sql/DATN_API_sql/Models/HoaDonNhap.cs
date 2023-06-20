namespace DATN_API_sql.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Xml.Serialization;
    using Newtonsoft.Json;


    [Table("HoaDonNhap")]
    public partial class HoaDonNhap
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HoaDonNhap()
        {
            ChiTietHDNs = new HashSet<ChiTietHDN>();
        }

        [Key]
        public int HdnID { get; set; }

        public int? MANCC { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NGAYTAO { get; set; }

        public int? TONGTIEN { get; set; }

        public int? NGUOITAO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        [XmlIgnore]
        public virtual ICollection<ChiTietHDN> ChiTietHDNs { get; set; }
        [JsonIgnore]
        [XmlIgnore]
        public virtual NhaCungCap NhaCungCap { get; set; }
        [JsonIgnore]
        [XmlIgnore]
        public virtual NhanVien NhanVien { get; set; }
    }
}
