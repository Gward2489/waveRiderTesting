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
        public async Task<IActionResult> GetClosestFullReport(string lat, string lon)
        {
            BuoyFinder buoyFinder = new BuoyFinder();            
            SpotFinder spotFinder = new SpotFinder();
            Beach closestSpot = spotFinder.FindSpot(lat, lon);
            List<Buoy> matchingBuoys = buoyFinder.MatchBuoys(closestSpot.Latitude, closestSpot.Longtitude);
            List<FullBeachReport> fullBeachReports = new List<FullBeachReport>();
            foreach(Buoy b in matchingBuoys)
            {
                FullReport fullReport = await Make45DayReport.GetAsync(b);
                FullBeachReport fullBeachReport = new FullBeachReport(closestSpot, fullReport);
                fullBeachReports.Add(fullBeachReport);
            }
            return Ok(fullBeachReports);
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

        [Route("api/[controller]/fullReports")]        
        [HttpGet("{spotId: int}")]
        public async Task<IActionResult> GetSingleFullReport(int spotId)
        {
            BuoyFinder buoyFinder = new BuoyFinder();            
            Beach beach = _context.Beach.Single(b => b.BeachId == spotId);
            List<Buoy> matchingBuoys = buoyFinder.MatchBuoys(beach.Latitude, beach.Longtitude);

            List<FullBeachReport> fullBeachReports = new List<FullBeachReport>();
            foreach (Buoy b in matchingBuoys)
            {
                FullReport fullReport = await Make45DayReport.GetAsync(b);
                FullBeachReport fullBeachReport = new FullBeachReport(beach, fullReport);
                fullBeachReports.Add(fullBeachReport);
            }

            return Ok (fullBeachReports);
        }
        [Route("api/[controller]/currentReport")]        
        [HttpGet("{lat}{lon}")]
        public async Task<IActionResult> GetClosestCurrentReport(string lat, string lon)
        {
            BuoyFinder buoyFinder = new BuoyFinder();            
            SpotFinder spotFinder = new SpotFinder();
            Beach closestSpot = spotFinder.FindSpot(lat, lon);
            List<Buoy> matchingBuoys = buoyFinder.MatchBuoys(closestSpot.Latitude, closestSpot.Longtitude);
            List<CurrentBeachReport> currentBeachReports = new List<CurrentBeachReport>();
            foreach(Buoy b in matchingBuoys)
            {
                CurrentReport currentReport = await MakeCurrentReport.GetAsync(b);
                CurrentBeachReport currentBeachReport = new CurrentBeachReport(closestSpot, currentReport);
                currentBeachReports.Add(currentBeachReport);
            }
            return Ok(currentBeachReports);
        }

        [Route("api/[controller]/currentReports")]
        [HttpGet("{lat}{lon}{spotCount: int}")]
        public async Task<IActionResult> GetClosestCurrentReports(string lat, string lon, int spotCount)
        {

            SpotFinder spotFinder = new SpotFinder();
            BuoyFinder buoyFinder = new BuoyFinder();

            List<Buoy> matchedBuoys = new List<Buoy>();
            List<CurrentBeachReport> currentBeachReport = new List<CurrentBeachReport>(); 
            List<CurrentReport> matchedBuoyReports = new List<CurrentReport>();
            
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
                CurrentReport currentReport = await MakeCurrentReport.GetAsync(b);
                Console.WriteLine("done");
                matchedBuoyReports.Add(currentReport);
            }

            foreach(SpotDistanceFromUser obj in spotsWithUserDistance)
            {
                string beachLat = obj.Beach.Latitude;
                string beachLon = obj.Beach.Longtitude;
                List<Buoy> matchingBuoys = buoyFinder.MatchBuoys(beachLat, beachLon);
                foreach(CurrentReport r in matchedBuoyReports)
                {
                    foreach(Buoy b in matchingBuoys)
                    {
                        if (r.NbdcId == b.NbdcId)
                        {
                        CurrentBeachReport report = new CurrentBeachReport(obj.Beach, r);
                        currentBeachReport.Add(report);
                        }
                    }
                }
            }
            return Ok(currentBeachReport);
        }

        [Route("api/[controller]/currentReports")]        
        [HttpGet("{spotId: int}")]
        public async Task<IActionResult> GetSingleCurrentReport(int spotId)
        {
            BuoyFinder buoyFinder = new BuoyFinder();            
            Beach beach = _context.Beach.Single(b => b.BeachId == spotId);
            List<Buoy> matchingBuoys = buoyFinder.MatchBuoys(beach.Latitude, beach.Longtitude);

            List<CurrentBeachReport> currentBeachReports = new List<CurrentBeachReport>();
            foreach (Buoy b in matchingBuoys)
            {
                CurrentReport currentReport = await MakeCurrentReport.GetAsync(b);
                CurrentBeachReport currentBeachReport = new CurrentBeachReport(beach, currentReport);
                currentBeachReports.Add(currentBeachReport);
            }

            return Ok (currentBeachReports);
        }
    }
}