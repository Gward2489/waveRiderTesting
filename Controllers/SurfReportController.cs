using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using waveRiderTester.CustomTypes;
using waveRiderTester.Data;
using waveRiderTester.GeoLocators;
using waveRiderTester.Models;
using waveRiderTester.ReportMakers;

namespace waveRiderTester.Controllers
{
    [EnableCors("AllowSpecificOrigin")]
    [Route("api/[controller]")]
    public class SurfReportController : Controller
    {

        private ApplicationDbContext _context;
        // Constructor method to create an instance of context to communicate with our database.
        public SurfReportController (ApplicationDbContext ctx) {
            _context = ctx;
        }
        
        [Route("api/[controller]/fullReport")]        
        [HttpGet("{lat}{lon}")]
        public IActionResult GetClosestReport(string lat, string lon)
        {
            SpotFinder spotFinder = new SpotFinder();
            Beach closestSpot = spotFinder.FindSpot(lat, lon);
            
                        

        }

        [Route("api/[controller]/fullReports")]
        [HttpGet("{lat}{lon}{spotCount: int}")]
        public async Task<IActionResult> GetClosestFullReports(string lat, string lon, int spotCount)
        {

            SpotFinder spotFinder = new SpotFinder();
            BuoyFinder buoyFinder = new BuoyFinder();

            List<Buoy> matchedBuoys = new List<Buoy>();
            List<FullBeachReport> fullBeachReport = new List<FullBeachReport>(); 
            List<FullReport> matchedBuoyReports = new List<FullReport>();
            
            List<SpotDistanceFromUser> spotsWithUserDistance = spotFinder.FindSpots(lat, lon, spotCount);

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

            matchedBuoys = matchedBuoys.GroupBy(mb => mb.BuoyId).Select(mb => mb.First()).ToList();

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

        [HttpGet("{spotId: int}")]
        public IActionResult GetSingleReport(int spotId)
        {

        }
    }
}