using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheRooms.Data;
using TheRooms.Models;
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

        [HttpGet]
        [Route("[controller]/{id:int}")]
        public async Task<IActionResult> Show(int id)
        {
            var token = Request.Cookies["token"];
            if (id != 1 && token == null || !HasAcess(token, id))
            {
                return BadRequest();
            }

            var room = await _context.Rooms
                    .Include(r => r.DoorsIn)
                    .Include(r => r.DoorsOut)
                    .SingleAsync(r => r.Id == id);

            var completed = _context.CompletedRooms.Any(c => c.RoomId == id && c.PlayerId == token);

            var model = new RoomViewModel(room, completed);
            return View(model);
        }

        [HttpPost]
        [Route("[controller]/{id:int}")]
        public IActionResult Submit(int id)
        {
            var answer = Request.Form["answer"].ToString();
            var token = GetToken(id, answer);

            if (token == null || !HasAcess(token, id))
            {
                return BadRequest();
            }

            if (AnswerIsCorrect(id, answer))
            {
                var completed = new CompletedRoom
                {
                    RoomId = id,
                    PlayerId = token
                };

                _context.CompletedRooms.Add(completed);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Show), new { id });
        }

        private bool AnswerIsCorrect(int id, string answer)
        {
            var room = _context.Rooms.Find(id);
            return room.Answer == "*" || room.Answer.ToLower() == answer;
        }

        private bool HasAcess(string token, int roomId)
        {
            if (roomId == 1) return true;

            var player = _context.Players
                .Include(p => p.CompletedRooms)
                .SingleOrDefault(p => p.Id == token);

            var doorToExists = _context.Doors
                .Any(d => d.RoomToId == roomId
                && player.CompletedRooms.Any(c => c.RoomId == d.RoomFromId));

            var doorFromExists = _context.Doors
                .Any(d => d.RoomFromId == roomId
                && player.CompletedRooms.Any(c => c.RoomId == d.RoomToId));

            return doorToExists || doorFromExists;
        }

        private string GetToken(int id, string answer)
        {
            var token = Request.Cookies["token"];
            if (token == null && id == 1)
            {
                token = Guid.NewGuid().ToString();
                var player = new Player
                {
                    Id = token,
                    Name = answer
                };

                _context.Players.Add(player);
                _context.SaveChanges();
                Response.Cookies.Append("token", token);
            }

            return token;
        }
    }
}