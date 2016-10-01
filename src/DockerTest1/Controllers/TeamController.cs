using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TasGenerator.Data;
using TasGenerator.Model;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TasGenerator.API.Controllers
{
    [Route("api/[controller]")]
    public class TeamController : Controller
    {
        ITasRepository db;

        public TeamController(ITasRepository repo)
        {
            db = repo;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<Team> Get()
        {
            return db.GetTeams();
        }

        // GET api/values/5
        [HttpGet("{name}")]
        public IActionResult Get(int id)
        {
            var team = db.GetTeam(id);
            if (team == null)
            {
                return  base.NotFound();
            }
            return new ObjectResult(team);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
