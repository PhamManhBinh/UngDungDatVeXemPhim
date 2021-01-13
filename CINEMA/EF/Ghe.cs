namespace CINEMA.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ghe")]
    public partial class Ghe
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ghe()
        {
            ChiTietVes = new HashSet<ChiTietVe>();
        }

        public int id { get; set; }

        public int idRap { get; set; }

        [Required]
        [StringLength(1)]
        public string Hang { get; set; }

        public int SoGhe { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietVe> ChiTietVes { get; set; }

        public virtual Rap Rap { get; set; }
    }
}
