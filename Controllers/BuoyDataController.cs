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
            string lat = "34.2141466";
            string lon = "-77.8998191";
            SpotFinder spotFinder = new SpotFinder();
            List<SpotDistanceFromUser> closestSpots = spotFinder.FindSpots(lat, lon);
            return Ok(closestSpots);
        }
    }
}