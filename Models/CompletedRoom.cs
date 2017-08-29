using System.ComponentModel.DataAnnotations.Schema;

namespace TheRooms.Models
{
    public class CompletedRoom
    {
        public int Id { get; set; }

        [ForeignKey("Room")]
        public int RoomId { get; set; }
        public Room Room { get; set; }

        [ForeignKey("Player")]
        public string PlayerId { get; set; }
        public Player Player { get; set; }
    }
}
