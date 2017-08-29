using Microsoft.EntityFrameworkCore;
using TheRooms.Models;

namespace TheRooms.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Door> Doors { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
    }
}
