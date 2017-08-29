using System.Collections.Generic;

namespace TheRooms.Models
{
    public class Player
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public ICollection<CompletedRoom> CompletedRooms { get; set; }
    }
}