using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using waveRiderTester.Data;
using waveRiderTester.Models;

// this controller allows users to request a list of beaches
// supported by wave rider. Users can request the full list or beaches
// by a specific state

namespace waveRiderTester.Controllers
{
    [EnableCors("AllowSpecificOrigin")]
    [Route("api/[controller]")]
    public class SurfSpotsController : Controller
    {

        private ApplicationDbContext _context;
        // Constructor method to create an instance of context to communicate with our database.
        public SurfSpotsController (ApplicationDbContext ctx) {
            _context = ctx;
        }

        // this is the route for the full list of beaches
        [HttpGet]
        public IActionResult Get()
        {
            // retreive beaches from db
            List<Beach> beaches = _context.Beach.ToList();

            // return beaches    
            return Ok(beaches);
        }

        //  this is the route to get list of beaches by state
        [HttpGet("{state}")]
        public IActionResult Get(string state)
        {
            // get beaches matching state
            List<Beach> stateBeaches = _context.Beach.Where(b => b.State == state).ToList();

            // return beaches
            return Ok(stateBeaches);
        }
    }
}