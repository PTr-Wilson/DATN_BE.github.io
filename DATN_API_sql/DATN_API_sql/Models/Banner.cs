
namespace DATN_API_sql.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Banner")]
    public partial class Banner
    {
        [Key]
        public int BANNER_ID { get; set; }

        [Required]
        [StringLength(200)]
        public string name { get; set; }

        [StringLength(500)]
        public string IMAGE { get; set; }

        [StringLength(500)]
        public string IMAGE1 { get; set; }

        [StringLength(500)]
        public string IMAGE2 { get; set; }
    }
}
