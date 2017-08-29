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
        public bool IsCompleted { get; set; }
        public IList<DoorViewModel> InDoors { get; set; } = new List<DoorViewModel>();
        public IList<DoorViewModel> OutDoors { get; set; } = new List<DoorViewModel>();

        public RoomViewModel(Room room, bool completed)
        {
            Id = room.Id;
            Name = room.Name;
            Task = room.Task;
            IsCompleted = completed;

            foreach (var door in room.DoorsIn)
            {
                InDoors.Add(new DoorViewModel
                {
                    RoomId = door.RoomFromId,
                });
            }

            foreach (var door in room.DoorsOut)
            {
                OutDoors.Add(new DoorViewModel
                {
                    RoomId = door.RoomToId,
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