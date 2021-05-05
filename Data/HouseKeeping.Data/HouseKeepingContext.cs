using HouseKeeping.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HouseKeeping.Data
{
    public class HouseKeepingContext : IdentityDbContext<AppUser, Role, string>
    {
        public DbSet<Location> Locations { get; set; }
        public DbSet<TaskCategory> TaskCategories { get; set; }
        public DbSet<TaskStatus> TaskStatuses { get; set; }
        public DbSet<Task> Tasks { get; set; }

        public HouseKeepingContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
