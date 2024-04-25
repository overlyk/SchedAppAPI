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
    public class GoalController : ControllerBase
    {
        private readonly AppDBContext _context;

        public GoalController(AppDBContext context)
        {
            _context = context;
        }

        //get single goal based on goal id
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Goal == null)
            {
                return NotFound();
            }

            var goal = await _context.Goal
                .FirstOrDefaultAsync(m => m.id == id);
            if (goal == null)
            {
                return NotFound();
            }

            return Ok(goal);
        }

        //get all goals
        [HttpGet]
        public async Task<IActionResult> Goals()
        {
            if (_context.Goal == null)
            {
                return NotFound();
            }

            var goals = await _context.Goal.ToListAsync();
            if (goals == null)
            {
                return NotFound();
            }

            return Ok(goals);
        }

        //create a goal
        [HttpPost("goal")]
        public async Task<IActionResult> Create([FromBody] Goal goal)
        {
            _context.Add(goal);
            await _context.SaveChangesAsync();
            return Ok(goal);
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


        [HttpPut("{goalId}")]
        public async Task<IActionResult> putGoal(int goalId,  Goal goal)
        {
            if (goalId != goal.id)
            {
                return BadRequest();
            }

            _context.Entry(goal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GoalExists(goalId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        [HttpPut("toggle")]
        public async Task<IActionResult> toggleConfirmed(Goal goal)
        {
            if (goal == null)
            {
                return BadRequest();
            }

            goal.isCompleted = !goal.isCompleted;
            _context.Entry(goal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GoalExists(goal.id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        [HttpDelete("{goalId}")]
        public async Task<IActionResult> DeleteConfirmed(int goalId)
        {
            try
            {
                var goal = await _context.Goal.FindAsync(goalId);
                if (goal != null)
                {
                    _context.Goal.Remove(goal);
                }
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private bool GoalExists(int id)
        {
          return (_context.Goal?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
