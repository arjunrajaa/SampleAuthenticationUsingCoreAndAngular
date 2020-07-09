using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Accessors
{
    public partial class UserDbContext : DbContext
    {
        public UserDbContext()
        {
        }

        public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(options)
        {

        }

        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>()
                .HasIndex(u => u.UserName)
                .IsUnique();
            modelBuilder.Entity<Roles>().HasData(
                 SeedRoles()
                 );
            modelBuilder.Entity<Users>().HasData(
                 SeedUser()
                 );
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        private static IList<Roles> SeedRoles()
        {
            return new List<Roles>()
            {
                new Roles()
                {
                    RoleId = 1,
                    Role= "Admin"
                },
                new Roles()
                {
                    RoleId = 2,
                    Role= "User"
                }
            };
        }

        private IList<Users> SeedUser()
        {
            return new List<Users>()
            {
                new Users()
                {
                    Uid = 1,
                    UserName ="admin@quaero.com",
                    Password = "Admin@1234",
                    FirstName= "Admin",
                    LastName = "Admin",
                    RoleId = 1,
                    DateOfBirth = DateTime.UtcNow
                }
            };
        }
    }
}
