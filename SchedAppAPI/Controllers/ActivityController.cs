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
    public class ActivityController : ControllerBase
    {
        private readonly AppDBContext _context;

        public ActivityController(AppDBContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Activity == null)
            {
                return NotFound();
            }

            var activity = await _context.Activity
                .FirstOrDefaultAsync(m => m.id == id);
            if (activity == null)
            {
                return NotFound();
            }

            return Ok(activity);
        }

        //get all activities in db
        [HttpGet]
        public async Task<IActionResult> Activities()
        {
            if (_context.Activity == null)
            {
                return NotFound();
            }

            var activities = await _context.Activity.ToListAsync();
            if (activities == null)
            {
                return NotFound();
            }

            return Ok(activities);
        }

        //create a new activity
        [HttpPost("activity")]
        public async Task<IActionResult> Create([FromBody] Activity activity)
        {
            _context.Add(activity);
            await _context.SaveChangesAsync();
            return Ok(activity);
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

        //// POST: Activities/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Activities == null)
        //    {
        //        return Problem("Entity set 'AppDBContext.Activities'  is null.");
        //    }
        //    var activity = await _context.Activities.FindAsync(id);
        //    if (activity != null)
        //    {
        //        _context.Activities.Remove(activity);
        //    }
            
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool ActivityExists(int id)
        //{
        //  return (_context.Activities?.Any(e => e.id == id)).GetValueOrDefault();
        //}
    }
}
