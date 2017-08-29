using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheRooms.Data;

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
            var room = await _context.Rooms.SingleAsync(r => r.Id == id);
            return View(room);
        }
    }
}