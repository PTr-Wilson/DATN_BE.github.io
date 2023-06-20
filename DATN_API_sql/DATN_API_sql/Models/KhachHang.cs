namespace DATN_API_sql.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Xml.Serialization;
    using Newtonsoft.Json;

    [Table("KhachHang")]
    public partial class KhachHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KhachHang()
        {
            DonHangs = new HashSet<DonHang>();
        }

        public int KhachHangID { get; set; }

        [StringLength(50)]
        public string TENKH { get; set; }

        public int? SODIENTHOAI { get; set; }

        [StringLength(250)]
        public string DIACHI { get; set; }

        [StringLength(50)]
        public string TAIKHOAN { get; set; }

        [StringLength(50)]
        public string MATKHAU { get; set; }

        [StringLength(50)]
        public string EMAIL { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        [XmlIgnore]
        public virtual ICollection<DonHang> DonHangs { get; set; }
    }
}
