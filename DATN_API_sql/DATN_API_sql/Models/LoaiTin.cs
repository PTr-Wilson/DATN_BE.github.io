namespace DATN_API_sql.Models
{
    using Newtonsoft.Json;

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Xml.Serialization;


    [Table("LoaiTin")]
    public partial class LoaiTin
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LoaiTin()
        {
            TinTucs = new HashSet<TinTuc>();
        }

        public int LoaiTinID { get; set; }

        [StringLength(150)]
        public string TENLOAITIN { get; set; }

        [StringLength(50)]
        public string TRANGTHAI { get; set; }

        public DateTime? CREATE_AT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        [XmlIgnore]
        public virtual ICollection<TinTuc> TinTucs { get; set; }
    }
}
