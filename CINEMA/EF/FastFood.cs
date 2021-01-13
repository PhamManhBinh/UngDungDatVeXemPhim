namespace CINEMA.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FastFood")]
    public partial class FastFood
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FastFood()
        {
            ChiTietVe_Food = new HashSet<ChiTietVe_Food>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Tên Món")]
        public string TenMon { get; set; }

        [Display(Name = "Mô tả")]
        [StringLength(500)]
        public string MoTa { get; set; }

        [Required]
        [Display(Name = "Giá")]
        public double Gia { get; set; }

        [Display(Name = "Hình Ảnh")]
        [Column(TypeName = "text")]
        public string Image { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietVe_Food> ChiTietVe_Food { get; set; }
    }
}
