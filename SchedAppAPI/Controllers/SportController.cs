using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchedAppAPI.Models;

namespace SchedAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SportController : ControllerBase
    {
        private readonly AppDBContext _context;

        public SportController(AppDBContext context)
        {
            _context = context;
        }

        //get a sport based on sport id
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sport == null)
            {
                return NotFound();
            }

            var sport = await _context.Sport
                .FirstOrDefaultAsync(m => m.id == id);
            if (sport == null)
            {
                return NotFound();
            }

            return Ok(sport);
        }

        //get all sports
        [HttpGet]
        public async Task<IActionResult> Sports()
        {
            if (_context.Sport == null)
            {
                return NotFound();
            }

            var sports = await _context.Sport.ToListAsync();
            if (sports == null)
            {
                return NotFound();
            }

            return Ok(sports);
        }

        //create a new sport
        [HttpPost("sport")]
        public async Task<IActionResult> Create([FromBody] Sport sport)
        {
            _context.Add(sport);
            await _context.SaveChangesAsync();
            return Ok(sport);
        }

        //// POST: Users/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("id,Name,Password")] User user)
        //{
        //    if (id != user.id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(user);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!UserExists(user.id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return Ok(user);
        //}

        //// POST: Users/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Users == null)
        //    {
        //        return Problem("Entity set 'AppDBContext.Users'  is null.");
        //    }
        //    var user = await _context.Users.FindAsync(id);
        //    if (user != null)
        //    {
        //        _context.Users.Remove(user);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool UserExists(int id)
        //{
        //  return (_context.Users?.Any(e => e.id == id)).GetValueOrDefault();
        //}
    }
}
