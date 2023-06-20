namespace DATN_API_sql.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Xml.Serialization;
    using Newtonsoft.Json;

    [Table("NhanVien")]
    public partial class NhanVien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NhanVien()
        {
            DonHangs = new HashSet<DonHang>();
            HoaDonNhaps = new HashSet<HoaDonNhap>();
            TinTucs = new HashSet<TinTuc>();
        }

        public int NhanVienID { get; set; }

        [StringLength(50)]
        public string TENNV { get; set; }

        [StringLength(50)]
        public string CHUCVU { get; set; }

        [StringLength(250)]
        public string DIACHI { get; set; }

        public int? SODIENTHOAI { get; set; }

        [StringLength(250)]
        public string EMAIL { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        [XmlIgnore]
        public virtual ICollection<DonHang> DonHangs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        [XmlIgnore]
        public virtual ICollection<HoaDonNhap> HoaDonNhaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        [XmlIgnore]
        public virtual ICollection<TinTuc> TinTucs { get; set; }
    }
}
