namespace CINEMA.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ve")]
    public partial class Ve
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ve()
        {
            ChiTietVes = new HashSet<ChiTietVe>();
            ChiTietVe_Food = new HashSet<ChiTietVe_Food>();
        }

        [Key]
        public int MaVe { get; set; }

        public int SuatChieuId { get; set; }

        public int UserId { get; set; }

        public DateTime ThoiDiemDatVe { get; set; }

        public double TongTien { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietVe> ChiTietVes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietVe_Food> ChiTietVe_Food { get; set; }

        public virtual SuatChieu SuatChieu { get; set; }

        public virtual User User { get; set; }
    }
}
