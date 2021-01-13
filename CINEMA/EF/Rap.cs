namespace CINEMA.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Rap")]
    public partial class Rap
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Rap()
        {
            Ghes = new HashSet<Ghe>();
            SuatChieux = new HashSet<SuatChieu>();
        }
        [Display(Name = "ID")]
        public int id { get; set; }

        [Required]
        [Display(Name = "Tên Rạp")]
        [StringLength(255)]
        public string TenRap { get; set; }

        [Required]
        [Display(Name = "Loại Rạp")]
        [StringLength(255)]
        public string LoaiRap { get; set; }

        [Display(Name = "Kích Thước Ngang")]
        [Range(1, 15)]
        public int KTNgang { get; set; }

        [Display(Name = "Kích Thước Dọc")]
        [Range(1, 15)]
        public int KTDoc { get; set; }

        public int? CumRapId { get; set; }

        public virtual CumRap CumRap { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ghe> Ghes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SuatChieu> SuatChieux { get; set; }
    }
}
