﻿using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(DataContext _dataContext) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetAll()
        {
            return Ok(await _dataContext.Users.ToListAsync());
        }

        [HttpGet("get-by-id/{id:int}")]
        public async Task<ActionResult<AppUser>> GetById([FromRoute] int id)
        {
            return Ok(await _dataContext.Users.SingleOrDefaultAsync(u => u.Id == id));
        }
    }
}