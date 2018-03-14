using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using waveRiderTester.Data;
using waveRiderTester.Models;

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

        [HttpGet]
        public IActionResult Get()
        {
            List<Beach> beaches = _context.Beach.ToList();
                        
            return Ok(beaches);
        }

        [HttpGet("{state}")]
        public IActionResult Get(string state)
        {
            List<Beach> stateBeaches = _context.Beach.Where(b => b.State == state).ToList();

            return Ok(stateBeaches);
        }
    }
}