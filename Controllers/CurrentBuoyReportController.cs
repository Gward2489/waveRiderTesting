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
            Buoy buoy = _context.Buoy.Single(b => b.NbdcId == nbdcId);
            CurrentReport currentReport = await MakeCurrentReport.GetAsync(buoy);

            return Ok(currentReport);

        }
    }
}