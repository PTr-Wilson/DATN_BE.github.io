namespace DATN_API_sql.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Xml.Serialization;
    using Newtonsoft.Json;

    [Table("LienHe")]
    public partial class LienHe
    {
        public int id { get; set; }

        [StringLength(250)]
        public string TENKHACHHANG { get; set; }

        [StringLength(50)]
        public string EMAIL { get; set; }

        public string NOIDUNG { get; set; }
    }
}
