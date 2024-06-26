﻿using System;
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
    public class UserController : ControllerBase
    {
        private readonly AppDBContext _context;

        public UserController(AppDBContext context)
        {
            _context = context;
        }

        //get single user based on user id
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.id == id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        //login user
        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody]User user)
        {

            var dbuser = await _context.User.Where(x => x.username == user.username).FirstOrDefaultAsync();
            if (dbuser != null)
            {
                if (dbuser.password == user.password)
                {
                    return Ok(dbuser);
                }
                else
                {
                    return BadRequest();
                }
            
            }
            else
            {
                return NotFound();
            }
        }

        //get all users in db
        [HttpGet]
        public async Task<IActionResult> Users()
        {
            if (_context.User == null)
            {
                return NotFound();
            }

            var users = await _context.User.ToListAsync();
            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }

        //create new user
        [HttpPost("user")]
        public async Task<IActionResult> Create([FromBody]User user)
        {
            var existingUser = await _context.User.Where(x => x.username == user.username).FirstOrDefaultAsync();
            if (existingUser == null) 
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return Ok(user);
            }
            else
            {
                return BadRequest();
            }

        }

        //// POST: Users/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("updateteam")]
        public async Task<IActionResult> Edit([FromBody]User user)
        {
            try
            {
                var dbuser = await _context.User.Where(x => x.id == user.id).FirstOrDefaultAsync();
                if (dbuser != null)
                {
                    dbuser.TeamId = user.TeamId;
                    _context.Update(dbuser);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch
            {
                return BadRequest();
            } 
        }
    }
}
