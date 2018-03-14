using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using waveRiderTester.Data;
using waveRiderTester.Models;
using System.Linq;
using waveRiderTester.CustomTypes;
using waveRiderTester.ReportMakers;
using System.Threading.Tasks;

namespace waveRiderTester.Controllers
{
    [EnableCors("AllowSpecificOrigin")]
    [Route("api/[controller]")]
    public class BuoyReportController : Controller
    {
        private ApplicationDbContext _context;
        // Constructor method to create an instance of context to communicate with our database.
        public BuoyReportController (ApplicationDbContext ctx) {
            _context = ctx;
        }


        [Route("api/[controller]/getCurrent")]
        [HttpGet("{nbdcId}")]
        public async Task<IActionResult> GetCurrentBuoyData(string nbcdId)
        {
            Buoy buoy = _context.Buoy.Single(b => b.NbdcId == nbcdId);
            CurrentReport currentReport = await MakeCurrentReport.GetAsync(buoy);

            return Ok(currentReport);

        }

        [Route("api/[controller]/get45day")]
        [HttpGet("{nbdcId}")]
        public async Task<IActionResult> Get45DayBuoyData(string nbcdId)
        {
            Buoy buoy = _context.Buoy.Single(b => b.NbdcId == nbcdId);
            FullReport fullReport = await Make45DayReport.GetAsync(buoy);
            
            return Ok(fullReport);
        }

        
    }
}