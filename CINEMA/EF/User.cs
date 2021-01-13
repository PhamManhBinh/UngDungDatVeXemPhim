namespace CINEMA.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Ves = new HashSet<Ve>();
        }

        [Display(Name = "ID")]
        public int id { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Họ Tên")]
        [StringLength(255)]
        public string Name { get; set; }

        [Column(TypeName = "text")]
        [Required]
        [Display(Name = "Số Điện Thoại")]
        public string Phone { get; set; }

        [Required]
        [Display(Name = "Giới Tính")]
        public bool Gender { get; set; }

        [Required]
        [Display(Name = "Ngày Sinh")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Birthday { get; set; }

        [Required]
        [Display(Name = "Quyền")]
        [StringLength(5)]
        public string Permission { get; set; } = "User";

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ve> Ves { get; set; }
    }
}
