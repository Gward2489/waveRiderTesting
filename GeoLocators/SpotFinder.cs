using System;
using System.Collections.Generic;
using System.Linq;
using GeoCoordinatePortable;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using waveRiderTester.CustomTypes;
using waveRiderTester.Data;
using waveRiderTester.Models;

namespace waveRiderTester.GeoLocators
{
    public class SpotFinder : DbContext
    {

        public List<SpotDistanceFromUser> FindSpots(string lat, string lon)
        {
            double userLat = Convert.ToDouble(lat);
            double userLon = Convert.ToDouble(lon);

            GeoCoordinate userLocation = new GeoCoordinate(userLat, userLon);
            List<SpotDistanceFromUser> distances = new List<SpotDistanceFromUser>();
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlite($"Filename=/home/gward2489/workspace/waveRiderTester/Data/waveDb.db");


            using (ApplicationDbContext context = new ApplicationDbContext(optionsBuilder.Options))
            {

                List<Beach> spots = context.Beach.ToList();


                foreach(Beach spot in spots)
                {
                    double spotLat = Convert.ToDouble(spot.Latitude);
                    double spotLon = Convert.ToDouble(spot.Longtitude);

                    GeoCoordinate spotLocation = new GeoCoordinate(spotLat, spotLon);

                    double metersToSpot = userLocation.GetDistanceTo(spotLocation);
                    SpotDistanceFromUser distanceObj = new SpotDistanceFromUser(metersToSpot,
                    spot);

                    if (distances.Count() < 5)
                    {
                        distances.Add(distanceObj);
                    }
                    else
                    {
                        
                        for (int i = 0; i < distances.Count(); i++)
                        {
                            if (metersToSpot < distances[i].DistanceToUser)
                            {
                                distances.Remove(distances[i]);
                                distances.Add(distanceObj);
                                break;
                            }
                        }
                    }
                }
            }
            return distances;
        }
    }
}