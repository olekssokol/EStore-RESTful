using EStore.Data;
using EStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace EStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        ApplicationContext db;
        public UserController(ApplicationContext context)
        {
            db = context;
            if (!db.User.Any())
            {
                db.User.Add(new User { Name = "Alex", Email = "alex2000@gmail.com", AccountStatus = true });
                db.User.Add(new User { Name = "Anna", Email = "2001anna@gmail.com", AccountStatus = false });
                db.User.Add(new User { Name = "Max", Email = "maxpro0205@gmail.com", AccountStatus = false });
                db.User.Add(new User { Name = "George", Email = "marvel1975@gmail.com", AccountStatus = false });
                db.User.Add(new User { Name = "Petter", Email = "potter007@gmail.com", AccountStatus = true });
                db.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            return await db.User.ToListAsync();
        }


        [HttpGet("{email}")]
        public async Task<ActionResult<User>> Get(string email)
        {
            User user = await db.User.FirstOrDefaultAsync(x => x.Email == email);
            if (user == null)
                return NotFound();
            return new ObjectResult(user);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            User user = await db.User.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
                return NotFound();
            return new ObjectResult(user);
        }


        // POST api/users
        [HttpPost]
        public async Task<ActionResult<User>> Post(User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            db.User.Add(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }

        
        [HttpPut]
        public async Task<ActionResult<User>> Put(User user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            if (!db.User.Any(x => x.Email == user.Email))
            {
                return NotFound();
            }

            db.Update(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }

        [HttpPut("{email}")]
        public async Task<ActionResult<User>> Put(User user, string email)
        {
            if (user.Email != email)
            {
                return BadRequest();
            }
            if (user == null)
            {
                return BadRequest();
            }
            if (!db.User.Any(x => x.Email == user.Email))
            {
                return NotFound();
            }

            db.Update(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> Delete(int id)
        {
            User user = db.User.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            db.User.Remove(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }

        [HttpDelete]
        public async Task<ActionResult<User>> Delete()
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection())
                {
                    connection.ConnectionString = "Host=localhost;Port=5433;Database=EStore;Username=postgres;Password=Admin";
                    connection.Open();
                    NpgsqlCommand cmd = new NpgsqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandText = "DELETE FROM public.\"User\" WHERE \"AccountStatus\" = false;";
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    connection.Close();
                }

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
