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
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Invoice> Invoices { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Quan hệ 1-1 giữa ApplicationUser và Person
            builder.Entity<ApplicationUser>()
                .HasOne(u => u.Person)
                .WithOne()
                .HasForeignKey<ApplicationUser>(u => u.PersonId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
