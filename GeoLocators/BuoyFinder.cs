using System;
using System.Collections.Generic;
using GeoCoordinatePortable;
using waveRiderTester.Models;

namespace waveRiderTester.GeoLocators
{
    public class BuoyFinder
    {
        public List<Buoy> MatchBuoys(string lat, string lon)
        {
            double beachLat = Convert.ToDouble(lat);
            double beachLon = Convert.ToDouble(lon);
            
            GeoCoordinate beachLocation = new GeoCoordinate(beachLat, beachLon);

            
        }
    }
}