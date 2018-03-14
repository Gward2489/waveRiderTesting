using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using waveRiderTester.CustomTypes;
using waveRiderTester.GeoLocators;
using waveRiderTester.Models;
using waveRiderTester.ReportMakers;

namespace waveRiderTester.Controllers
{
    [EnableCors("AllowSpecificOrigin")]
    [Route("api/[controller]")]
    public class BuoyDataController: Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            string lat = "34.2379067";
            string lon = "-77.8484527";

            SpotFinder spotFinder = new SpotFinder();
            BuoyFinder buoyFinder = new BuoyFinder();

            List<CurrentBeachReport> currentReports = new List<CurrentBeachReport>();
            List<FullBeachReport> fullReports = new List<FullBeachReport>();
            
            List<SpotDistanceFromUser> spotsWithUserDistance = spotFinder.FindSpots(lat, lon, 5);

            foreach(SpotDistanceFromUser obj in spotsWithUserDistance)
            {
                string beachLat = obj.Beach.Latitude;
                string beachLon = obj.Beach.Longtitude;
                List<Buoy> matchingBuoys = buoyFinder.MatchBuoys(beachLat, beachLon);
                foreach(Buoy b in matchingBuoys)
                {
                    // CurrentReport currentReport = await MakeCurrentReport.GetAsync(b);

                    // CurrentBeachReport beachAndReport = new CurrentBeachReport(obj.Beach, 
                    // currentReport);

                    // currentReports.Add(beachAndReport);

                    FullReport fullReport = await Make45DayReport.GetAsync(b);
                    FullBeachReport beachAndReport = new FullBeachReport(obj.Beach,
                    fullReport);
                    fullReports.Add(beachAndReport);
                }    
            }

            return Ok(fullReports);
        }
    }
}