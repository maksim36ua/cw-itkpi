using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw_itkpi.Models
{
    public class UserContext : DbContext
    {
        public DbSet<UserInfo> Users { get; set; }
        //public DbSet<WeeklyRatingClass> WeeklyRatings { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //{
        //    options.UseSqlServer(@"Data Source=itkpicw.database.windows.net;Initial Catalog=codewars_db;Integrated Security=False;User ID=maksim36ua;Password=Rtt713Fz;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        //}
        public UserContext(DbContextOptions options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Make NameId required
            modelBuilder.Entity<UserInfo>()
                .Property(b => b.username)
                .IsRequired();
        }
        
    }
}
