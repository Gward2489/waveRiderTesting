using System;
using System.Collections.Generic;
using System.Linq;
using GeoCoordinatePortable;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using waveRiderTester.CustomTypes;
using waveRiderTester.Data;
using waveRiderTester.Models;

// this class will find the closest beach or a given number of beaches by geolocation

namespace waveRiderTester.GeoLocators
{
    public class SpotFinder
    {
        // method to find given number of beaches
        public List<SpotDistanceFromUser> FindSpots(string lat, string lon, int spotCount)
        {

            // convert lat and long to doubles 
            double userLat = Convert.ToDouble(lat);
            double userLon = Convert.ToDouble(lon);

            // create a geocoordinate instance for the users location
            GeoCoordinate userLocation = new GeoCoordinate(userLat, userLon);

            // create an empty list to hold distances
            List<SpotDistanceFromUser> distances = new List<SpotDistanceFromUser>();

            // create an optionsBuilder object for the db context
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            // use sqlite and point to our db
            optionsBuilder.UseSqlite($"Filename=/home/gward2489/workspace/waveRiderTester/Data/waveDb.db");

            // use the db context
            using (ApplicationDbContext context = new ApplicationDbContext(optionsBuilder.Options))
            {
                // get beaches from db
                List<Beach> spots = context.Beach.ToList();

                // itterate through the beaches
                foreach(Beach spot in spots)
                {
                    // convert beach lat/lon to doubles
                    double spotLat = Convert.ToDouble(spot.Latitude);
                    double spotLon = Convert.ToDouble(spot.Longtitude);

                    // create a geocoordinate instance for the current beach
                    GeoCoordinate spotLocation = new GeoCoordinate(spotLat, spotLon);

                    // get distance between current beach and user
                    double metersToSpot = userLocation.GetDistanceTo(spotLocation);

                    // create an instance of SpotDistanceFromUser holding the distance we
                    // just calculated, as well as beach name
                    SpotDistanceFromUser distanceObj = new SpotDistanceFromUser(metersToSpot,
                    spot);

                    // the followng logic adds the closest beaches to the distances list

                    // add the current beach to the distances list, if the distances
                    // list contains less than the specified amount of request beaches

                    if (distances.Count() < spotCount)
                    {
                        distances.Add(distanceObj);
                    } 
                    // if distances already contains the max amount of spots...
                    else
                    {
                        // itterate through distances
                        for (int i = 0; i < distances.Count(); i++)
                        {
                            // if the current spot is closer than a spot already in the list...
                            if (metersToSpot < distances[i].DistanceToUser)
                            {
                                // remove the spot that is farther away
                                distances.Remove(distances[i]);
                                // add the closer spot
                                distances.Add(distanceObj);
                                // break the loop so we don't remove more than one
                                // beach at a time and end up with duplicate values
                                break;
                            }
                        }
                    }
                }
            }

            distances = distances.OrderBy(d => d.DistanceToUser).ToList();
            return distances;
        }

        // this method finds the single closest surf spot, requires lat/long of user
        public Beach FindSpot(string lat, string lon)
        {
            // convert user lat/long to doubles
            double userLat = Convert.ToDouble(lat);
            double userLon = Convert.ToDouble(lon);

            // create a new instance of a beach Model to hold closest beach
            Beach closestBeach = new Beach();

            // create ageocoordinate instance for the users location
            GeoCoordinate userLocation = new GeoCoordinate(userLat, userLon);

            // create an optionsBuilder class for the db context
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            // use sqlite and point to out db
            optionsBuilder.UseSqlite($"Filename=/home/gward2489/workspace/waveRiderTester/Data/waveDb.db");
            // use the context
            using (ApplicationDbContext context = new ApplicationDbContext(optionsBuilder.Options))
            {
                // get list of beaches from database
                List<Beach> spots = context.Beach.ToList();

                // declare a double that will hold a distance measurement used to
                // to compare beach distances
                double lastDistanceFromUser = 0;

                // declare a counter
                double count = 0;

                // itterate through beaches
                foreach(Beach spot in spots)
                {   
                    // convert beach lat/long to doubles
                    double spotLat = Convert.ToDouble(spot.Latitude);
                    double spotLon = Convert.ToDouble(spot.Longtitude);

                    // create a geocoordinate instance for the current beach
                    GeoCoordinate spotLocation = new GeoCoordinate(spotLat, spotLon);

                    // get distance between user and beach
                    double distanceFromUser = userLocation.GetDistanceTo(spotLocation);

                    // if count is 0 ...
                    if (count == 0)
                    {
                        // increase count
                        count ++;
                        // set current spot to closest spot
                        closestBeach = spot;
                        // set distance reference to current distance
                        lastDistanceFromUser = distanceFromUser;
                    }

                    // if the distance between the current beach and the user is less than 
                    // the last distance...
                    if (distanceFromUser < lastDistanceFromUser)
                    {
                        // set closest beach to current beach
                        closestBeach = spot;
                        // set distance refence to current distance
                        lastDistanceFromUser = distanceFromUser;
                    }
                }
            }

            // return closest beach 
            return closestBeach;
        }
    }
}