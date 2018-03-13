using System;
using System.Collections.Generic;
using System.Linq;
using GeoCoordinatePortable;
using Microsoft.EntityFrameworkCore;
using waveRiderTester.Data;
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
            
            List<Buoy> matchedBuoys = new List<Buoy>();

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlite($"Filename=/home/gward2489/workspace/waveRiderTester/Data/waveDb.db");

            using (ApplicationDbContext context = new ApplicationDbContext(optionsBuilder.Options))
            {
                List<Buoy> buoys = context.Buoy.ToList();

                foreach(Buoy b in buoys)
                {
                    double buoyLat = Convert.ToDouble(b.Latitude);
                    double buoyLon = Convert.ToDouble(b.Longtitude);

                    GeoCoordinate buoyLocation = new GeoCoordinate(buoyLat, buoyLon);

                    double distanceToBuoy = beachLocation.GetDistanceTo(buoyLocation);

                    if (distanceToBuoy < 24140.2)
                    {
                        matchedBuoys.Add(b);
                    }
                }
            }

            return matchedBuoys;
        }
    }
}