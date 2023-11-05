using Microsoft.AspNetCore.Mvc;
using RI_UserAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace RI_UserAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private static readonly List<RI_User> _riUsers = new List<RI_User>();

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_riUsers);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var user = _riUsers.FirstOrDefault(u => u.Id == id);
            return user != null ? Ok(user) : NotFound();
        }

        [HttpPost]
        public IActionResult Create(RI_User user)
        {

            // TO DO : consider to move to the validation service  
            var today = DateTime.Today;
            var age = today.Year - user.DateOfBirth.Year;
            // If the user's birthday hasn't occurred yet this year, subtract one.
            if (user.DateOfBirth > today.AddYears(-age)) age--;

            if (age < 18)
            {
                return BadRequest("User must be 18 years or older.");
            }

            if (_riUsers.Any(u => u.Email == user.Email))
            {
                return BadRequest("Email must be unique.");
            }

            user.Id = Guid.NewGuid();
            _riUsers.Add(user);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, RI_User user)
        {
            var index = _riUsers.FindIndex(u => u.Id == id);
            if (index == -1)
            {
                return NotFound();
            }

            user.Id = id;
            _riUsers[index] = user;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var index = _riUsers.FindIndex(u => u.Id == id);
            if (index == -1)
            {
                return NotFound();
            }

            _riUsers.RemoveAt(index);
            return NoContent();
        }
    }
}
