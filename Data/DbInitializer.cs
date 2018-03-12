using System;
using System.Linq;
using System.Xml;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using waveRiderTester.HttpCalls;
using waveRiderTester.Models;

namespace waveRiderTester.Data
{
    public class DbInitializer
    {
        public static async void InitializeBuoysAsync(IServiceProvider serviceProvider)
        {
           using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
           {
                if (context.Buoy.Any())
                {
                    return;   // DB has been seeded
                }

                XmlDocument buoyXml = await GetBuoyListXml.FetchAsync();
                XmlNodeList buoyList = buoyXml.GetElementsByTagName("station");
                foreach(XmlNode buoy in buoyList)
                {
                    string buoyId = buoy.Attributes["id"].Value;
                    string lat = buoy.Attributes["lat"].Value;
                    string lon = buoy.Attributes["lon"].Value;
                    string name = buoy.Attributes["name"].Value;
                    string owner = buoy.Attributes["owner"].Value;
                    Buoy newBuoy = new Buoy(buoyId, lat, lon, name, owner);
                    context.Buoy.Add(newBuoy);                                                            
                }

                context.SaveChanges();

           } 
        }
        public static void InitializeBeachesAsync(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Beach.Any())
                {
                    return;   // DB has been seeded
                }

                var Beaches = new Beach[]
                {
                    new Beach ("Breakwater", "26.3341576", "-80.0752845", "Boca Inlet", "Florida", "Southern"),
                    new Beach ("Reef", "26.3987043", "-80.0686133", "Highland Beach / Jap Rock", "Florida", "Southern"),
                    new Beach ("Beach", "27.4679928", "-80.300824", "Fort Pierce Jetty", "Florida", "Southern"),
                    new Beach ("Reef", "26.6131723", "-80.0363343", "Lake Worth Pier", "Florida", "Southern"),
                    new Beach ("Beach", "27.863507", "-80.4526853", "Sebastian Inlet", "Florida", "Southern"),
                    new Beach ("Beach", "25.7818876", "-80.1527164", "South Beach", "Florida", "Southern"),
                    new Beach ("Beach", "27.2088126", "-80.1855357", "Stuart Beach", "Florida", "Southern"),
                    new Beach ("Beach", "26.8933116", "-80.0579899", "Juno Beach Pier", "Florida", "Southern"),
                    new Beach ("Beach", "26.3091625", "-80.0837752", "Deerfield Beach", "Florida", "Southern"),
                    new Beach ("Beach", "26.1721868", "-80.1269806", "Vista Park / Ft. Lauderdale", "Florida", "Southern"),
                    new Beach ("Beach", "28.0684823", "-80.5743447", "Melbourne Beach", "Florida", "Southern"),
                    new Beach ("Beach", "26.9195654", "-80.0773954", "Jupiter Beach", "Florida", "Southern"),
                    new Beach ("Beach", "29.8502506", "-81.2832897", "St. Augustine Beach", "Florida", "Northern"),
                    new Beach ("Beach", "29.0697169", "-80.9271867", "New Smyrna Inlet", "Florida", "Northern"),
                    new Beach ("Beach", "29.0461519", "-80.9165927", "New Smyrna (Saphire St.)", "Florida", "Northern"),
                    new Beach ("Beach", "30.1621965", "-81.3736127", "Mickler's Landing Beach", "Florida", "Northern"),
                    new Beach ("Beach", "30.4292474", "-81.4245727", "Little Talbot Island", "Florida", "Northern"),
                    new Beach ("Beach", "30.2934523", "-81.3913275", "Jacksonville Beach Pier", "Florida", "Northern"),
                    new Beach ("Beach", "30.3808714", "-81.4151537", "Kathryn Abbey Hanna Park", "Florida", "Northern"),
                    new Beach ("Beach", "29.4823428", "-81.1451647", "Flagler Beach", "Florida", "Northern"),
                    new Beach ("Beach", "29.2278588", "-81.0248837", "Daytona Beach", "Florida", "Northern"),
                    new Beach ("Beach", "29.7782316", "-81.2725477", "Crescent Beach", "Florida", "Northern"),
                    new Beach ("Beach", "28.3677945", "-80.6203461", "Cocoa Beach Pier", "Florida", "Northern"),
                    new Beach ("Beach", "32.0067085", "-80.8972558", "Tybee Island North", "Georgia", "Northern"),
                    new Beach ("Beach", "31.9896377", "-80.8656707", "Tybee Island South", "Georgia", "Northern"),
                    new Beach ("Beach", "31.2983808", "-81.3910733", "Little Saint Simons Island (Pen Island)", "Georgia", "Southern"),
                    new Beach ("Beach", "31.148189", "-81.4012345", "Saint Simons Island", "Georgia", "Southern"),
                    
                    
                };


            }
        }
    }
}