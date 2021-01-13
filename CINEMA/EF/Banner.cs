namespace CINEMA.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Banner")]
    public partial class Banner
    {
        public int id { get; set; }

        [Display(Name = "Hình Ảnh")]
        [Column(TypeName = "text")]
        public string Image { get; set; }

        [Required]
        [Display(Name = "Liên Kết")]
        [Column(TypeName = "text")]
        public string Link { get; set; }
    }
}
