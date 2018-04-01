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

// This controller allows users to request surf reports by geolocation, or by 
// beach id

namespace waveRiderTester.Controllers
{
    [EnableCors("AllowSpecificOrigin")]
    [Route("api/[controller]")]
    public class CurrentSurfReportController : Controller
    {
        private ApplicationDbContext _context;
        // Constructor method to create an instance of context to communicate with our database.
        public CurrentSurfReportController (ApplicationDbContext ctx) {
            _context = ctx;
        }

        //this route will return the surf report for the beach closest
        //to the given lat/long
        [HttpGet("{lat}/{lon}")]
        public async Task<IActionResult> GetClosestCurrentReport(string lat, string lon)
        {
            // create new instances of bouyFinder and spotFinder classes
            BuoyFinder buoyFinder = new BuoyFinder();            
            SpotFinder spotFinder = new SpotFinder();
            // get the closest beach by using the lat/long passed in by the user
            Beach closestSpot = spotFinder.FindSpot(lat, lon);
            // get a list of buoys that are close to the beach
            List<Buoy> matchingBuoys = buoyFinder.MatchBuoys(closestSpot.Latitude, closestSpot.Longtitude);
            // create empty list for hold current beach reports
            List<CurrentBeachReport> currentBeachReports = new List<CurrentBeachReport>();
            //  itterate through the list of buoys
            foreach(Buoy b in matchingBuoys)
            {
                // get report from current buoy 
                CurrentReport currentReport = await MakeCurrentReport.GetAsync(b);
                // compile data from closestSpot object and currentReport object into
                // currentBeachReport object with contructor function
                CurrentBeachReport currentBeachReport = new CurrentBeachReport(closestSpot, currentReport);
                // add beach report object to list
                currentBeachReports.Add(currentBeachReport);
            }

            // return reports 
            return Ok(currentBeachReports);
        }

        // this route will return a list containing reports for the closest beaches,
        // amount of beaches returns corresponds to given spotCount int
        [HttpGet("{lat}/{lon}/{spotCount}")]
        public async Task<IActionResult> GetClosestCurrentReports(string lat, string lon, int spotCount)
        {

            // create new instances of spot finder and buoy finder classes
            SpotFinder spotFinder = new SpotFinder();
            BuoyFinder buoyFinder = new BuoyFinder();

            // create empty lists to hold matched buoys, currentBeachReports
            // and matchedBuoyReports
            List<Buoy> matchedBuoys = new List<Buoy>();
            List<CurrentReport> matchedBuoyReports = new List<CurrentReport>();
            List<CurrentBeachReport> currentBeachReport = new List<CurrentBeachReport>(); 

            // get the list of spots corresponding to user location and spotCount int
            List<SpotDistanceFromUser> spotsWithUserDistance = spotFinder.FindSpots(lat, lon, spotCount);

            // itterate through objects containing 
            // beach info and distance from users location. 
            // This loop will populate the matchedBuoys list
            // with buoys near the users location
            foreach(SpotDistanceFromUser obj in spotsWithUserDistance)
            {  
                // get lat and long for current beach 
                string beachLat = obj.Beach.Latitude;
                string beachLon = obj.Beach.Longtitude;

                // get a list of buoys that are within range of the current beach
                List<Buoy> matchingBuoys = buoyFinder.MatchBuoys(beachLat, beachLon);

                // itterate through the matching buoys
                foreach(Buoy b in matchingBuoys)
                {
                    // add current buoy to list of matched buoys 
                    matchedBuoys.Add(b);
                }
            }

            // using linq statements, eliminate duplicate buoys. 
            // this groups the buoys their id, then selects only 
            // one buoy if there are multiple buoys with the same id.
            matchedBuoys = matchedBuoys.GroupBy(mb => mb.BuoyId).Select(mb => mb.First()).ToList();

            // itterate through the list of buoys close to the user.
            // this loop populates the list of matched buoy reports
            foreach(Buoy b in matchedBuoys)
            {
                // get the report for the current buoy
                CurrentReport currentReport = await MakeCurrentReport.GetAsync(b);
                // add report to list
                matchedBuoyReports.Add(currentReport);
            }

            // itterate again through the list of beaches close to the user.
            // This loop will populate our currentBeachReport list. This logic is
            // executed independently of the first itteration of this list to avoid
            // making duplicate http calls for buoys
            foreach(SpotDistanceFromUser obj in spotsWithUserDistance)
            {
                // get lat and long of current beach
                string beachLat = obj.Beach.Latitude;
                string beachLon = obj.Beach.Longtitude;

                // get buoys in proximity of beach
                List<Buoy> matchingBuoys = buoyFinder.MatchBuoys(beachLat, beachLon);

                // itterate through the list of buoy reports
                foreach(CurrentReport r in matchedBuoyReports)
                {
                    // itterate through the list of matching buoys
                    foreach(Buoy b in matchingBuoys)
                    {
                        // if a buoy report matches one of the buoys in user proximity...
                        if (r.NbdcId == b.NbdcId)
                        {
                            // create a new currentBeachReport Object and return it
                            CurrentBeachReport report = new CurrentBeachReport(obj.Beach, r);
                            currentBeachReport.Add(report);
                        }
                    }
                }
            }
            return Ok(currentBeachReport);
        }

        // this route will return report data for a surf spot that corresponds to the
        // given spotId int
        [HttpGet("{spotId}")]
        public async Task<IActionResult> GetSingleCurrentReport(int spotId)
        {
            // create a new instance of the buoy finder class
            BuoyFinder buoyFinder = new BuoyFinder();
            
            // find the beach requested            
            Beach beach = _context.Beach.Single(b => b.BeachId == spotId);

            // find buoys closest to the beach
            List<Buoy> matchingBuoys = buoyFinder.MatchBuoys(beach.Latitude, beach.Longtitude);

            // create an empty list to hold beach reports
            List<CurrentBeachReport> currentBeachReports = new List<CurrentBeachReport>();

            // itterate through the matching buoys.
            // this loop will populate our currentBeachReports list
            foreach (Buoy b in matchingBuoys)
            {
                // generate report for current buoy
                CurrentReport currentReport = await MakeCurrentReport.GetAsync(b);

                // create beach report with buoy report data and beach data
                CurrentBeachReport currentBeachReport = new CurrentBeachReport(beach, currentReport);

                // add beach report to the list
                currentBeachReports.Add(currentBeachReport);
            }

            // return the report data
            return Ok (currentBeachReports);
        } 
    }
}