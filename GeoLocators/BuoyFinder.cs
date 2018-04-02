using System;
using System.Collections.Generic;
using System.Linq;
using GeoCoordinatePortable;
using Microsoft.EntityFrameworkCore;
using waveRiderTester.Data;
using waveRiderTester.Models;

// this class geolocates buoys available through NOAA

namespace waveRiderTester.GeoLocators
{
    public class BuoyFinder
    {
        // method to return list of buoys requires lat an lon
        public List<Buoy> MatchBuoys(string lat, string lon)
        {
            // convert lat and long to doubles
            double beachLat = Convert.ToDouble(lat);
            double beachLon = Convert.ToDouble(lon);

            // create geocoordinate instance for given beach
            GeoCoordinate beachLocation = new GeoCoordinate(beachLat, beachLon);
            
            // create an empty list to hold matching buoys
            List<Buoy> matchedBuoys = new List<Buoy>();

            // create an optionsBuilder object
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            // add sqlite context
            optionsBuilder.UseSqlite($"Filename=/home/gward2489/workspace/waveRiderTester/Data/waveDb.db");

            // access the context
            using (ApplicationDbContext context = new ApplicationDbContext(optionsBuilder.Options))
            {
                // get list of buoys from db
                List<Buoy> buoys = context.Buoy.ToList();

                // itterate through the list of buoys
                foreach(Buoy b in buoys)
                {
                    // convert buoy coord to doubles
                    double buoyLat = Convert.ToDouble(b.Latitude);
                    double buoyLon = Convert.ToDouble(b.Longtitude);

                    // create new geocoordinate instance for buoy
                    GeoCoordinate buoyLocation = new GeoCoordinate(buoyLat, buoyLon);

                    // get distance from buoy to beach
                    double distanceToBuoy = beachLocation.GetDistanceTo(buoyLocation);

                    // if buoy is within given range, add it to the matching buoys list.
                    // the int represents distance in meters. Change it to increase or decrease
                    // radius buoys will be pulled from
                    if (distanceToBuoy < 24140.2)
                    {
                        matchedBuoys.Add(b);
                    }
                }
            }

            // return matching buoys
            return matchedBuoys;
        }
    }
}