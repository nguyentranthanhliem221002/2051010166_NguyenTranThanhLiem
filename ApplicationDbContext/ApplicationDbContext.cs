using _2051010166_NguyenTranThanhLiem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace _2051010166_NguyenTranThanhLiem
{
    //public class ApplicationDbContext : DbContext
    //{
    //    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    //      : base(options)
    //    {
    //    }
    //    public DbSet<Base> Bases { get; set; }
    //    public DbSet<Role> Roles { get; set; }
    //    public DbSet<Person> Persons { get; set; }


    //}
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<ApartmentInfo> ApartmentInfos { get; set; }
        public DbSet<SystemSetting> SystemSettings { get; set; }

        public DbSet<Invoice> Invoices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User <-> Vehicle
            modelBuilder.Entity<Vehicle>()
                .HasOne(v => v.Person)
                .WithMany(u => u.Vehicles)
                .HasForeignKey(v => v.PersonId)
                .OnDelete(DeleteBehavior.Restrict); // không xóa user -> xóa vehicle

            // Apartment <-> Vehicle
            modelBuilder.Entity<Vehicle>()
                .HasOne(v => v.Apartment)
                .WithMany(a => a.Vehicles)
                .HasForeignKey(v => v.ApartmentId)
                .OnDelete(DeleteBehavior.Cascade); // xóa Apartment -> xóa tất cả Vehicle

            // VehicleType <-> Vehicle
            modelBuilder.Entity<Vehicle>()
                .HasOne(v => v.VehicleType)
                .WithMany(vt => vt.Vehicles)
                .HasForeignKey(v => v.VehicleTypeId)
                .OnDelete(DeleteBehavior.Restrict); // loại xe vẫn tồn tại

            // Apartment <-> User
            modelBuilder.Entity<Apartment>()
                .HasOne(a => a.Person)
                .WithMany(u => u.Apartments)
                .HasForeignKey(a => a.PersonId)
                .OnDelete(DeleteBehavior.Restrict); // không xóa user khi xóa apartment
        }

    }
}
