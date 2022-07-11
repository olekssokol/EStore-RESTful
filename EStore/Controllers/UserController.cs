using EStore.Data;
using EStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
                db.User.Add(new User { Name = "Bro", Email = "sadasds@gmail.com", AccountStatus = true });
                db.User.Add(new User { Name = "DDDD", Email = "dssdsd@gmail.com", AccountStatus = false });
                db.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            return await db.User.ToListAsync();
        }

        
        [HttpGet("{id}")]
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

        // PUT api/users/
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

        // DELETE api/users/5
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
    }
}
