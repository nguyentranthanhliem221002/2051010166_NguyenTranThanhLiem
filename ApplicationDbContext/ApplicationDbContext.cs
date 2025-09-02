using _2051010166_NguyenTranThanhLiem.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace _2051010166_NguyenTranThanhLiem.ApplicationDbContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
          : base(options)
        {
        }
        public DbSet<Base> Bases { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Person> Persons { get; set; }


    }
}
