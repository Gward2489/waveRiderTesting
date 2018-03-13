using System;
using System.Collections.Generic;
using System.Linq;
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
    public class BuoyDataTwoController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            string lat = "34.2379067";
            string lon = "-77.8484527";

            List<Buoy> matchedBuoys = new List<Buoy>();
            List<FullBeachReport> fullBeachReport = new List<FullBeachReport>(); 
            List<FullReport> matchedBuoyReports = new List<FullReport>();

            SpotFinder spotFinder = new SpotFinder();
            BuoyFinder buoyFinder = new BuoyFinder();

            List<SpotDistanceFromUser> spotsWithUserDistance = spotFinder.FindSpots(lat, lon);
            foreach(SpotDistanceFromUser obj in spotsWithUserDistance)
            {   
                string beachLat = obj.Beach.Latitude;
                string beachLon = obj.Beach.Longtitude;
                List<Buoy> matchingBuoys = buoyFinder.MatchBuoys(beachLat, beachLon);
                foreach(Buoy b in matchingBuoys)
                {

                    matchedBuoys.Add(b);

                }
            }

            matchedBuoys = matchedBuoys.GroupBy(x => x.BuoyId).Select(x => x.First()).ToList();

      
            foreach(Buoy b in matchedBuoys)
            {
                Console.WriteLine("hello");
                FullReport fullReport = await Make45DayReport.GetAsync(b);
                Console.WriteLine("done");
                matchedBuoyReports.Add(fullReport);
            }

            foreach(SpotDistanceFromUser obj in spotsWithUserDistance)
            {
                string beachLat = obj.Beach.Latitude;
                string beachLon = obj.Beach.Longtitude;
                List<Buoy> matchingBuoys = buoyFinder.MatchBuoys(beachLat, beachLon);
                foreach(FullReport r in matchedBuoyReports)
                {
                    foreach(Buoy b in matchingBuoys)
                    {
                        if (r.NbdcId == b.NbdcId)
                        {
                        FullBeachReport report = new FullBeachReport(obj.Beach, r);
                        fullBeachReport.Add(report);
                        }
                    }
                }
            }
            return Ok(fullBeachReport);
        }
    }
}