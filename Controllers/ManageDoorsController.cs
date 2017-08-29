using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheRooms.Data;
using TheRooms.Models;

namespace TheRooms.Controllers
{
    public class ManageDoorsController : Controller
    {
        private readonly DataContext _context;

        public ManageDoorsController(DataContext context)
        {
#if !DEBUG //dirty hack to prevent access to admin page
                throw new Exception("You can manage objects only from debug");
#endif
            _context = context;
        }

        // GET: ManageDoors
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.Doors.Include(d => d.RoomFrom).Include(d => d.RoomTo);
            return View(await dataContext.ToListAsync());
        }

        // GET: ManageDoors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var door = await _context.Doors
                .Include(d => d.RoomFrom)
                .Include(d => d.RoomTo)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (door == null)
            {
                return NotFound();
            }

            return View(door);
        }

        // GET: ManageDoors/Create
        public IActionResult Create()
        {
            ViewData["RoomFromId"] = new SelectList(_context.Rooms, "Id", "Id");
            ViewData["RoomToId"] = new SelectList(_context.Rooms, "Id", "Id");
            return View();
        }

        // POST: ManageDoors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RoomFromId,RoomToId")] Door door)
        {
            if (ModelState.IsValid)
            {
                _context.Add(door);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoomFromId"] = new SelectList(_context.Rooms, "Id", "Id", door.RoomFromId);
            ViewData["RoomToId"] = new SelectList(_context.Rooms, "Id", "Id", door.RoomToId);
            return View(door);
        }

        // GET: ManageDoors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var door = await _context.Doors.SingleOrDefaultAsync(m => m.Id == id);
            if (door == null)
            {
                return NotFound();
            }
            ViewData["RoomFromId"] = new SelectList(_context.Rooms, "Id", "Id", door.RoomFromId);
            ViewData["RoomToId"] = new SelectList(_context.Rooms, "Id", "Id", door.RoomToId);
            return View(door);
        }

        // POST: ManageDoors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RoomFromId,RoomToId")] Door door)
        {
            if (id != door.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(door);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoorExists(door.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoomFromId"] = new SelectList(_context.Rooms, "Id", "Id", door.RoomFromId);
            ViewData["RoomToId"] = new SelectList(_context.Rooms, "Id", "Id", door.RoomToId);
            return View(door);
        }

        // GET: ManageDoors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var door = await _context.Doors
                .Include(d => d.RoomFrom)
                .Include(d => d.RoomTo)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (door == null)
            {
                return NotFound();
            }

            return View(door);
        }

        // POST: ManageDoors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var door = await _context.Doors.SingleOrDefaultAsync(m => m.Id == id);
            _context.Doors.Remove(door);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoorExists(int id)
        {
            return _context.Doors.Any(e => e.Id == id);
        }
    }
}
