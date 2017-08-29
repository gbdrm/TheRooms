using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheRooms.Data;
using TheRooms.ViewModels;

namespace TheRooms.Controllers
{
    public class RoomsController : Controller
    {
        private readonly DataContext _context;
        public RoomsController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Show(int id)
        {
            var room = await _context.Rooms
                .Include(r => r.DoorsIn)
                .Include(r => r.DoorsOut)
                .SingleAsync(r => r.Id == id);
            var model = new RoomViewModel(room);
            return View(model);
        }
    }
}