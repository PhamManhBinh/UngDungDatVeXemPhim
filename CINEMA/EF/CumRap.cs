namespace CINEMA.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CumRap")]
    public partial class CumRap
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CumRap()
        {
            Raps = new HashSet<Rap>();
        }
        [Display(Name = "ID")]
        public int id { get; set; }

        [Required]
        [Display(Name = "Tên Cụm")]
        [StringLength(255)]
        public string TenCum { get; set; }

        [Required]
        [Display(Name = "Địa Chỉ")]
        [StringLength(255)]
        public string DiaChi { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Maps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rap> Raps { get; set; }
    }
}
