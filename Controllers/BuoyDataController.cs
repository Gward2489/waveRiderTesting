using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using waveRiderTester.CustomTypes;
using waveRiderTester.GeoLocators;
using waveRiderTester.Models;

namespace waveRiderTester.Controllers
{
    [Route("api/[controller]")]
    public class BuoyDataController: Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            string lat = "34.2379067";
            string lon = "-77.8484527";
            SpotFinder spotFinder = new SpotFinder();
            List<SpotDistanceFromUser> distances = spotFinder.FindSpots(lat, lon);

            


            BuoyFinder buoyFinder = new BuoyFinder();
            List<Buoy> closestSpots = buoyFinder.MatchBuoys(lat, lon);
            return Ok(closestSpots);
        }
    }
}