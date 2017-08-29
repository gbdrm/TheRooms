using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheRooms.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Task { get; set; }
        public string Answer { get; set; }

        [InverseProperty("RoomTo")]
        public ICollection<Door> DoorsIn { get; set; }

        [InverseProperty("RoomFrom")]
        public ICollection<Door> DoorsOut { get; set; }

        public ICollection<CompletedRoom> CompletedRooms { get; set; }
    }
}
