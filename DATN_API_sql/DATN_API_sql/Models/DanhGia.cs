namespace DATN_API_sql.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Xml.Serialization;
    using Newtonsoft.Json;

    [Table("DanhGia")]
    public partial class DanhGia
    {
        public int id { get; set; }

        public int? MASANPHAM { get; set; }

        public int? SODIEM { get; set; }

/*        public string TENSP { get; set; }
*/
        [StringLength(250)]
        public string BINHLUAN { get; set; }
        [JsonIgnore]
        [XmlIgnore]
        public virtual SanPham SanPham { get; set; }
    }
}
