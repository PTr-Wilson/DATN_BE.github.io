namespace DATN_API_sql.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Xml.Serialization;

    [Table("TinTuc")]
    public partial class TinTuc
    {
        public int TinTucID { get; set; }

        public int? NhanVienID { get; set; }

        public string TIEUDE { get; set; }

        public string NOIDUNG { get; set; }

        [StringLength(250)]
        public string IMAGE { get; set; }

        public string TRICHDAN { get; set; }

        public int? LoaiTinID { get; set; }

        [StringLength(50)]
        public string TRANGTHAI { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NGAYDANG { get; set; }
        [NotMapped]
        public string TENLOAITIN { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public virtual LoaiTin LoaiTin { get; set; }
        [JsonIgnore]
        [XmlIgnore]
        public virtual NhanVien NhanVien { get; set; }
    }
}
