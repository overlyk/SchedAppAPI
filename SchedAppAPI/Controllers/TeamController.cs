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
    public class TeamController : ControllerBase
    {
        private readonly AppDBContext _context;

        public TeamController(AppDBContext context)
        {
            _context = context;
        }

        //Get Team
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Team == null)
            {
                return NotFound();
            }

            var team = await _context.Team
                .FirstOrDefaultAsync(m => m.id == id);
            if (team == null)
            {
                return NotFound();
            }

            return Ok(team);
        }

        //get all teams in db
        [HttpGet]
        public async Task<IActionResult> Teams()
        {
            if (_context.Team == null)
            {
                return NotFound();
            }
            var teams = await _context.Team.ToListAsync();
            if (teams == null)
            {
                return NotFound();
            }
            return Ok(teams);
        }
        // POST: Create Team
        [HttpPost("team")]
        public async Task<IActionResult> Create([FromBody] Team team)
        {
            _context.Add(team);
            await _context.SaveChangesAsync();
            return Ok(team);
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
