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
    public class GameController : Controller
    {
        private readonly AppDBContext _context;

        public GameController(AppDBContext context)
        {
            _context = context;
        }

        //get a game based on its id
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Game == null)
            {
                return NotFound();
            }
            var game = await _context.Game
                .FirstOrDefaultAsync(m => m.id == id);
            if (game == null)
            {
                return NotFound();
            }
            return Ok(game);
        }

        //get all games
        [HttpGet]
        public async Task<IActionResult> Games()
        {
            if (_context.Game == null)
            {
                return NotFound();
            }

            var games = await _context.Game.ToListAsync();
            if (games == null)
            {
                return NotFound();
            }

            return Ok(games);
        }
        //create a new game
        [HttpPost("game")]
        public async Task<IActionResult> Create([FromBody] Game game)
        {
            _context.Add(game);
            await _context.SaveChangesAsync();
            return Ok(game);
        }

        //// POST: Activities/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("id,Name,StartTime,EndTime,UserID")] Activity activity)
        //{
        //    if (id != activity.id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(activity);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ActivityExists(activity.id))
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
        //    return Ok(activity);
        //}

        [HttpDelete("{gameId}")]
        public async Task<IActionResult> DeleteConfirmed(int gameId)
        {
            try
            {
                var game = await _context.Game.FindAsync(gameId);
                if (game != null)
                {
                    _context.Game.Remove(game);
                }
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
