namespace CINEMA.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Phim")]
    public partial class Phim
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Phim()
        {
            SuatChieux = new HashSet<SuatChieu>();
        }
        [Display(Name = "ID")]
        public int id { get; set; }

        [Required]
        [Display(Name = "Tên Phim")]
        [StringLength(255)]
        public string Ten { get; set; }

        [StringLength(255)]
        [Display(Name = "Ảnh Poster")]
        public string Poster { get; set; }

        [Required]
        [Display(Name = "Trailer")]
        [StringLength(255)]
        public string TraiLer { get; set; }

        [Display(Name = "Thời Lượng")]
        public int ThoiLuong { get; set; }

        [Required]
        [Display(Name = "Đạo Diễn")]
        [StringLength(255)]
        public string DaoDien { get; set; }

        [Required]
        [Display(Name = "Diễn Viên")]
        [StringLength(255)]
        public string DienVien { get; set; }

        [Required]
        [Display(Name = "Thể Loại")]
        [StringLength(255)]
        public string TheLoai { get; set; }

        [Required]
        [Display(Name = "Ngày Công Chiếu")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? NgayCongChieu { get; set; }

        [StringLength(3000)]
        public string NoiDungPhim { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SuatChieu> SuatChieux { get; set; }
    }
}
