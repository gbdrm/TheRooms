using System.Collections.Generic;
using TheRooms.Models;

namespace TheRooms.ViewModels
{
    public class RoomViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Task { get; set; }
        public string Reaction { get; set; }
        public IList<DoorViewModel> OutDoors { get; set; } = new List<DoorViewModel>();

        public RoomViewModel(Room room)
        {

            Id = room.Id;
            Name = room.Name;
            Task = room.Task;

            foreach (var door in room.DoorsOut)
            {
                OutDoors.Add(new DoorViewModel
                {
                    RoomId = door.RoomToId,
                    //RoomName = door.RoomTo.Name
                });
            }
        }
    }

    public class DoorViewModel
    {
        public int RoomId { get; set; }
        public string RoomName { get; set; }
    }
}