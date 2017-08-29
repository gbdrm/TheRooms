using System.ComponentModel.DataAnnotations.Schema;

namespace TheRooms.Models
{
    public class Door
    {
        public int Id { get; set; }

        [ForeignKey("RoomFrom")]
        public int RoomFromId { get; set; }
        public Room RoomFrom { get; set; }

        [ForeignKey("RoomTo")]
        public int RoomToId { get; set; }
        public Room RoomTo { get; set; }
    }
}
