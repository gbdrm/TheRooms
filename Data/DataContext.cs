using System.Linq;
using Microsoft.EntityFrameworkCore;
using TheRooms.Models;

namespace TheRooms.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Door> Doors { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<CompletedRoom> CompletedRooms { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // disable cascade delete
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
