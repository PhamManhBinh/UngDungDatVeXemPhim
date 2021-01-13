namespace CINEMA.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CinemaDbContext : DbContext
    {
        public CinemaDbContext()
            : base("name=CinemaDbContext")
        {
        }

        public virtual DbSet<Banner> Banners { get; set; }
        public virtual DbSet<ChiTietVe> ChiTietVes { get; set; }
        public virtual DbSet<ChiTietVe_Food> ChiTietVe_Food { get; set; }
        public virtual DbSet<CumRap> CumRaps { get; set; }
        public virtual DbSet<FastFood> FastFoods { get; set; }
        public virtual DbSet<Ghe> Ghes { get; set; }
        public virtual DbSet<Phim> Phims { get; set; }
        public virtual DbSet<Rap> Raps { get; set; }
        public virtual DbSet<SuatChieu> SuatChieux { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Ve> Ves { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Banner>()
                .Property(e => e.Image)
                .IsUnicode(false);

            modelBuilder.Entity<Banner>()
                .Property(e => e.Link)
                .IsUnicode(false);

            modelBuilder.Entity<CumRap>()
                .Property(e => e.Maps)
                .IsUnicode(false);

            modelBuilder.Entity<FastFood>()
                .Property(e => e.Image)
                .IsUnicode(false);

            modelBuilder.Entity<FastFood>()
                .HasMany(e => e.ChiTietVe_Food)
                .WithRequired(e => e.FastFood)
                .HasForeignKey(e => e.FoodId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ghe>()
                .Property(e => e.Hang)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Ghe>()
                .HasMany(e => e.ChiTietVes)
                .WithRequired(e => e.Ghe)
                .HasForeignKey(e => e.MaGhe)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Rap>()
                .HasMany(e => e.Ghes)
                .WithRequired(e => e.Rap)
                .HasForeignKey(e => e.idRap)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SuatChieu>()
                .HasMany(e => e.Ves)
                .WithRequired(e => e.SuatChieu)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Ves)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ve>()
                .HasMany(e => e.ChiTietVes)
                .WithRequired(e => e.Ve)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ve>()
                .HasMany(e => e.ChiTietVe_Food)
                .WithRequired(e => e.Ve)
                .WillCascadeOnDelete(false);
        }
    }
}
