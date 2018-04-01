using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using waveRiderTester.Data;
using waveRiderTester.Models;
using System.Linq;
using waveRiderTester.CustomTypes;
using waveRiderTester.ReportMakers;
using System.Threading.Tasks;

// This controller allows for a user to request current buoy data by passing in
// a buoy id 

namespace waveRiderTester.Controllers
{
    [EnableCors("AllowSpecificOrigin")]
    [Route("api/[controller]")]
    public class CurrentBuoyReportController : Controller
    {
        private ApplicationDbContext _context;
        // Constructor method to create an instance of context to communicate with our database.
        public CurrentBuoyReportController (ApplicationDbContext ctx) {
            _context = ctx;
        }

        [HttpGet("{nbdcId}")]
        public async Task<IActionResult> GetCurrentBuoyData(string nbdcId)
        {
            // retreive buoy information from database 
            Buoy buoy = _context.Buoy.Single(b => b.NbdcId == nbdcId);

            // use buoy data to retreive current report data
            CurrentReport currentReport = await MakeCurrentReport.GetAsync(buoy);

            // return current report
            return Ok(currentReport);

        }
    }
}