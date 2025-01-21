using GestionTerrains.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace GestionTerrains.Data
{
    public class ReservationDbContext : DbContext
    {
        public ReservationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Terrain> Terrains { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Staff> Staffs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Terrain>()
                .HasOne(t => t.Responsable)
                .WithMany()
                .HasForeignKey(t => t.StaffId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Terrain)
                .WithMany()
                .HasForeignKey(r => r.TerrainId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}




