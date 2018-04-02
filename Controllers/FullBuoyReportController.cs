using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using waveRiderTester.Data;
using waveRiderTester.Models;
using System.Linq;
using waveRiderTester.CustomTypes;
using waveRiderTester.ReportMakers;
using System.Threading.Tasks;
using System;

// this controller allows users to request 45 day report data 
// from a buoy 
namespace waveRiderTester.Controllers
{
    [EnableCors("AllowSpecificOrigin")]
    [Route("api/[controller]")]
    public class FullBuoyReportController : Controller
    {
        private ApplicationDbContext _context;
        // Constructor method to create an instance of context to communicate with our database.
        public FullBuoyReportController (ApplicationDbContext ctx) {
            _context = ctx;
        }


        [HttpGet("{nbdcId}")]
        public async Task<IActionResult> Get45DayBuoyData(string nbdcId)
        {
            // find requested buoy
            Buoy buoy = _context.Buoy.Single(b => b.NbdcId == nbdcId);

            // get report data for buoy
            FullReport fullReport = await Make45DayReport.GetAsync(buoy);
            
            // return report data
            return Ok(fullReport);
        }

        
    }
}