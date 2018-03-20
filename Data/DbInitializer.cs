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
                    // new Beach ("Breakwater", "26.3341576", "-80.0752845", "Boca Inlet", "Florida", "Southern"),
                    // new Beach ("Reef", "26.3987043", "-80.0686133", "Highland Beach / Jap Rock", "Florida", "Southern"),
                    new Beach ("Beach", "27.4679928", "-80.300824", "Fort Pierce Jetty", "Florida", "Southern"),
                    // new Beach ("Reef", "26.6131723", "-80.0363343", "Lake Worth Pier", "Florida", "Southern"),
                    // new Beach ("Beach", "27.863507", "-80.4526853", "Sebastian Inlet", "Florida", "Southern"),
                    // new Beach ("Beach", "25.7818876", "-80.1527164", "South Beach", "Florida", "Southern"),
                    // new Beach ("Beach", "27.2088126", "-80.1855357", "Stuart Beach", "Florida", "Southern"),
                    // new Beach ("Beach", "26.8933116", "-80.0579899", "Juno Beach Pier", "Florida", "Southern"),
                    // new Beach ("Beach", "26.3091625", "-80.0837752", "Deerfield Beach", "Florida", "Southern"),
                    // new Beach ("Beach", "26.1721868", "-80.1269806", "Vista Park / Ft. Lauderdale", "Florida", "Southern"),
                    // new Beach ("Beach", "28.0684823", "-80.5743447", "Melbourne Beach", "Florida", "Southern"),
                    // new Beach ("Beach", "26.9195654", "-80.0773954", "Jupiter Beach", "Florida", "Southern"),
                    // new Beach ("Beach", "29.8502506", "-81.2832897", "St. Augustine Beach", "Florida", "Northern"),
                    // new Beach ("Beach", "29.0697169", "-80.9271867", "New Smyrna Inlet", "Florida", "Northern"),
                    // new Beach ("Beach", "29.0461519", "-80.9165927", "New Smyrna (Saphire St.)", "Florida", "Northern"),
                    // new Beach ("Beach", "30.1621965", "-81.3736127", "Mickler's Landing Beach", "Florida", "Northern"),
                    // new Beach ("Beach", "30.4292474", "-81.4245727", "Little Talbot Island", "Florida", "Northern"),
                    // new Beach ("Beach", "30.2934523", "-81.3913275", "Jacksonville Beach Pier", "Florida", "Northern"),
                    // new Beach ("Beach", "30.3808714", "-81.4151537", "Kathryn Abbey Hanna Park", "Florida", "Northern"),
                    // new Beach ("Beach", "29.4823428", "-81.1451647", "Flagler Beach", "Florida", "Northern"),
                    // new Beach ("Beach", "29.2278588", "-81.0248837", "Daytona Beach", "Florida", "Northern"),
                    // new Beach ("Beach", "29.7782316", "-81.2725477", "Crescent Beach", "Florida", "Northern"),
                    new Beach ("Beach", "28.3677945", "-80.6203461", "Cocoa Beach Pier", "Florida", "Northern"),
                    // new Beach ("Beach", "32.0067085", "-80.8972558", "Tybee Island North", "Georgia", "Northern"),
                    // new Beach ("Beach", "31.9896377", "-80.8656707", "Tybee Island South", "Georgia", "Northern"),
                    // new Beach ("Beach", "31.2983808", "-81.3910733", "Little Saint Simons Island (Pen Island)", "Georgia", "Southern"),
                    // new Beach ("Beach", "31.148189", "-81.4012345", "Saint Simons Island", "Georgia", "Southern"),
                    // new Beach ("Beach", "32.6646744", "-79.9349177", "Folly Beach", "South Carolina", "Southern"),
                    // new Beach ("Beach", "32.1940571", "-80.7064202", "Hilton Head (Burkes Beach)", "South Carolina", "Southern"),
                    // new Beach ("Beach", "33.5772118", "-79.0163346", "Murrells Inlet (Garden City Beach)", "South Carolina", "Northern"),
                    // new Beach ("Beach", "32.7872014", "-79.8008407", "Isle of Palms", "South Carolina", "Northern"),
                    // new Beach ("Beach", "33.608106", "-78.9865307", "Myrtle Beach (Surfside Beach)", "South Carolina", "Northern"),
                    // new Beach ("Beach", "33.7052253", "-78.8649655", "Myrtle Beach (27th ave)", "South Carolina", "Northern"),
                    new Beach ("Beach", "33.8867209", "-78.4552517", "Ocean Isle Beach", "North Carolina", "Southern"),
                    new Beach ("Beach", "33.9121419", "-78.3070927", "Holden Beach", "North Carolina", "Southern"),
                    new Beach ("Beach", "34.0306678", "-77.9106797", "Carolina Beach", "North Carolina", "Southern"),
                    new Beach ("Beach", "34.1627258", "-77.8471397", "Masonboro Insland", "North Carolina", "Southern"),
                    new Beach ("Beach", "34.2037637", "-77.8139237", "Wrightsville Beach", "North Carolina", "Southern"),
                    // new Beach ("Beach", "34.4247527", "-77.5616607", "Topsail Beach / Surf City", "North Carolina", "Southern"),
                    // new Beach ("Beach", "34.5731646", "-77.2827437", "Camp Lejune / Sneads Ferry", "North Carolina", "Southern"),
                    // new Beach ("Beach", "34.6677445", "-77.0239117", "Emerald Isle", "North Carolina", "Southern"),
                    // new Beach ("Beach", "34.6977645", "-76.7523147", "Atlantic Beach", "North Carolina", "Southern"),
                    // new Beach ("Beach", "35.2280662", "-75.5463587", "Hatteras Island", "North Carolina", "Outer Banks"),
                    // new Beach ("Beach", "35.6213939", "-75.6276076", "Rodanthe", "North Carolina", "Outer Banks"),
                    // new Beach ("Beach", "35.940614", "-75.6341307", "Nags Head", "North Carolina", "Outer Banks"),
                    new Beach ("Beach", "36.1009029", "-75.7296607", "Kitty Hawk", "North Carolina", "Outer Banks"),
                    new Beach ("Beach", "36.3755388", "-75.8410947", "Corolla", "North Carolina", "Outer Banks"),
                    new Beach ("Beach", "36.8555585", "-75.9937987", "Virginia Beach", "Virginia", "Southern"),
                    // new Beach ("Beach", "36.7179916", "-75.9514277", "Sandbridge", "Virginia", "Southern"),
                    new Beach ("Beach", "37.9067411", "-75.3487247", "Chincoteague", "Virginia", "Northern"),
                    // new Beach ("Beach", "38.3534608", "-75.0916617", "Ocean City", "Maryland", "Central"),
                    // new Beach ("Beach", "38.2213969", "-75.1604467", "Assateague", "Maryland", "Central"),
                    // new Beach ("Beach", "38.5339027", "-75.0718697", "Bethany Beach", "Delaware", "Central"),
                    // new Beach ("Rivermouth", "38.6121147", "-75.0807727", "Indian River Inlet", "Delaware", "Central"),
                    // new Beach ("Beach", "38.7094267", "-75.0918067", "Dewey Beach", "Delaware", "Central"),
                    // new Beach ("Beach", "38.9320806", "-74.9269267", "Cape May", "New Jersey", "Southern"),
                    // new Beach ("Beach", "39.1552814", "-74.7070267", "Sea Isle City", "New Jersey", "Southern"),
                    // new Beach ("Beach", "39.2504654", "-74.6265147", "Ocean City", "New Jersey", "Southern"),
                    // new Beach ("Beach", "39.3616644", "-74.4314657", "Atlantic City", "New Jersey", "Southern"),
                    // new Beach ("Beach", "39.6484282", "-74.1914607", "Long Beach", "New Jersey", "Northern"),
                    // new Beach ("Beach", "39.5488033", "-74.2666499", "Beach Haven", "New Jersey", "Northern"),
                    // new Beach ("Beach", "39.9422969", "-74.0731446", "Seaside Heights", "New Jersey", "Northern"),
                    // new Beach ("Beach", "40.089765", "-74.0546047", "Point Pleasant", "New Jersey", "Northern"),
                    // new Beach ("Beach", "40.2180089", "-74.0176637", "Asbury Park", "New Jersey", "Northern"),
                    // new Beach ("Beach", "40.3383628", "-73.9923117", "Monmouth Beach", "New Jersey", "Northern"),
                    // new Beach ("Beach", "40.4684318", "-74.0162817", "North Beach", "New Jersey", "Northern"),
                    // new Beach ("Beach", "40.5750228", "-73.8607347", "Rockaway Beach", "New York", "Central"),
                    // new Beach ("Beach", "40.5846997", "-73.6509047", "Long Beach", "New York", "Central"),
                    // new Beach ("Beach", "40.5962847", "-73.5065057", "Jones Beach", "New York", "Central"),
                    // new Beach ("Beach", "40.6930757", "-72.9959747", "Fire Island", "New York", "Central"),
                    // new Beach ("Beach", "40.7772526", "-72.7262077", "Cupsogue (West Hampton)", "New York", "Central"),
                    // new Beach ("Beach", "40.8435056", "-72.4824417", "Shinnecock Park (Flies)", "New York", "Central"),
                    // new Beach ("Beach", "40.8435056", "-72.4824417", "Shinnecock Park (Flies)", "New York", "Central"),
                    // new Beach ("Beach", "41.05851", "-71.9558392", "Montauk", "New York", "Central"),
                    // new Beach ("Beach", "41.1722943", "-71.6307776", "Block Island", "Rhode Island", "Central"),
                    // new Beach ("Beach", "41.3279974", "-71.7766247", "Weekapaug", "Rhode Island", "Central"),
                    // new Beach ("Beach", "41.3119056", "-71.8352822", "Misquamicut", "Rhode Island", "Central"),
                    // new Beach ("Beach", "41.3767883", "-71.5481457", "Matunuck", "Rhode Island", "Central"),
                    // new Beach ("Beach", "41.3727933", "-71.5175047", "Point Judith", "Rhode Island", "Central"),
                    // new Beach ("Beach", "41.4397133", "-71.4643987", "Narragansett Town Beach", "Rhode Island", "Central"),
                    // new Beach ("Beach", "41.4564743", "-71.3297097", "Newport", "Rhode Island", "Central"),
                    // new Beach ("Beach", "41.4874113", "-71.2741437", "Middletown", "Rhode Island", "Central"),
                    // new Beach ("Beach", "41.4816995", "-71.1906267", "South Shore Beach", "Rhode Island", "Central"),
                    // new Beach ("Beach", "41.3565189", "-70.6760191", "Long Point (Martha's Vineyard)", "Massachusetts", "Central"),
                    // new Beach ("Beach", "41.3497634", "-70.5311237", "South Beach (Martha's Vineyard)", "Massachusetts", "Central"),
                    new Beach ("Beach", "41.2416447", "-70.1084559", "Nantucket", "Massachusetts", "Central"),
                    // new Beach ("Beach", "41.7882461", "-69.9523717", "Nauset Beach", "Massachusetts", "Central"),
                    // new Beach ("Beach", "41.947736", "-70.6466487", "Plymouth", "Massachusetts", "Central"),
                    // new Beach ("Beach", "42.4228688", "-70.9441897", "Nahant", "Massachusetts", "Central"),
                    // new Beach ("Beach", "42.9864412", "-70.7640186", "Jennes State Beach", "New Hampshire", "Central"),
                    // new Beach ("Beach", "43.3020613", "-70.5840866", "Wells", "Maine", "Central"),
                    new Beach ("Beach", "43.5628533", "-70.2504806", "Portland (Cape Elizabeth)", "Maine", "Central"),
                    // new Beach ("Beach", "32.5848334", "-117.1502667", "Imperial Beach", "California", "Southern"),
                    new Beach ("Reef", "32.6680614", "-117.2610617", "Point Loma", "California", "Southern"),
                    new Beach ("Reef", "32.7189494", "-117.2739937", "Sunset Cliffs", "California", "Southern"),
                    new Beach ("Beach", "32.7541084", "-117.2702177", "Ocean Beach (San Diego)", "California", "Southern"),
                    new Beach ("Beach", "32.7961444", "-117.2746617", "Crystal Pier (La Jolla)", "California", "Southern"),
                    new Beach ("Beach", "32.8604874", "-117.2729287", "La Jolla Shores", "California", "Southern"),
                    new Beach ("Beach", "32.8873903", "-117.2707727", "Black's Beach", "California", "Southern"),
                    new Beach ("Beach", "32.9638813", "-117.2862497", "Del Mar", "California", "Southern"),
                    new Beach ("Reef", "33.0022503", "-117.2958407", "Seaside Reef (Solana)", "California", "Southern"),
                    new Beach ("Beach", "33.0354763", "-117.3120647", "Swamis (Encinitas)", "California", "Southern"),
                    new Beach ("Beach", "33.1594292", "-117.357292", "Ocean St (Carlsbad)", "California", "Southern"),
                    new Beach ("Beach", "33.1947732", "-117.4026517", "Oceanside Pier", "California", "Southern"),
                    // new Beach ("Beach", "33.4185821", "-117.6368907", "San Celemente Pier", "California", "Southern"),
                    // new Beach ("Beach", "33.4616971", "-117.7137957", "Dana Point", "California", "Southern"),
                    // new Beach ("Beach", "33.539645", "-117.7994657", "Laguna Beach", "California", "Southern"),
                    new Beach ("Beach", "33.604666", "-117.9330657", "Newport Beach", "California", "Southern"),
                    new Beach ("Beach", "33.662291", "-118.0291067", "Huntington Beach", "California", "Southern"),
                    new Beach ("Beach", "33.7641899", "-118.1938217", "Long Beach", "California", "Southern"),
                    new Beach ("Beach", "34.0148958", "-118.5194017", "Santa Monica", "California", "Southern"),
                    new Beach ("Beach", "34.0374568", "-118.6936297", "Malibu", "California", "Southern"),
                    // new Beach ("Beach", "34.0679638", "-119.0242027", "Point Mugu", "California", "Southern"),
                    new Beach ("Beach", "34.1549168", "-119.2379437", "Point Hueneme", "California", "Southern"),
                    new Beach ("Beach", "34.4096266", "-119.7075117", "Santa Barbara", "California", "Southern"),
                    new Beach ("Beach", "34.2931377", "-119.3424377", "Ventura", "California", "Southern"),
                    // new Beach ("Beach", "34.6822035", "-120.6225367", "Surf Beach (Santa Barbara)", "California", "Southern"),
                    new Beach ("Beach", "35.1381466", "-120.6714553", "Pismo Beach", "California", "Southern"),
                    new Beach ("Beach", "35.3754952", "-120.8794507", "Morro Bay", "California", "Southern"),
                    new Beach ("Beach", "36.351304", "-122.2543723", "Moss Landing", "California", "Northern"),
                    // new Beach ("Beach", "36.9542826", "-122.0355259", "Santa Cruz", "California", "Northern"),
                    // new Beach ("Reef", "37.4958282", "-122.5155867", "Mavericks (Half Moon Bay)", "California", "Northern"),
                    new Beach ("Beach", "37.5962398", "-122.5036633", "Pacifica State Beach", "California", "Northern"),
                    new Beach ("Beach", "37.7608051", "-122.5291917", "Ocean Beach (San Francisco)", "California", "Northern"),
                    new Beach ("Beach", "37.90213", "-122.6694947", "Stinson Beach", "California", "Northern"),
                    // new Beach ("Beach", "38.2512979", "-122.9854757", "Dillon Beach", "California", "Northern"),
                    new Beach ("Beach", "38.3570248", "-123.0849457", "Salmon Creek", "California", "Northern"),
                    // new Beach ("Beach", "38.6974493", "-123.4434057", "Pebble Creek / Black Point Beach", "California", "Northern"),
                    // new Beach ("Beach", "38.9165356", "-123.7322217", "Point Area", "California", "Northern"),
                    new Beach ("Beach", "39.3611653", "-123.8345807", "Caspar Beach", "California", "Northern"),
                    new Beach ("Beach", "39.4611563", "-123.8262837", "Fort Bragg / Virgin Beach", "California", "Northern"),
                    // new Beach ("Beach", "40.022301", "-124.0856187", "Shelter Cover / Cape Mendocino Lighthouse", "California", "Northern"),
                    new Beach ("Beach", "40.7980007", "-124.2242077", "Eureka", "California", "Northern"),
                    new Beach ("Beach", "41.7486791", "-124.2144667", "Crescent City", "California", "Northern"),
                    // new Beach ("Beach", "42.064582", "-124.3232387", "Brookings", "Oregon", "Southern"),
                    // new Beach ("Beach", "42.4106308", "-124.4432097", "Gold Beach", "Oregon", "Southern"),
                    new Beach ("Reef", "43.3889404", "-124.3282166", "Coos Bay", "Oregon", "Southern"),
                    new Beach ("Beach", "43.6764832", "-124.2214456", "Winchester Bay", "Oregon", "Southern"),
                    // new Beach ("Beach", "44.028658", "-124.1538216", "Florence", "Oregon", "Southern"),
                    new Beach ("Beach", "44.6299177", "-124.0833706", "Newport", "Oregon", "Northern"),
                    // new Beach ("Beach", "44.9744715", "-124.0341156", "Lincoln City", "Oregon", "Northern"),
                    // new Beach ("Beach", "45.3324243", "-123.9879166", "Cape Lookout", "Oregon", "Northern"),
                    // new Beach ("Beach", "45.886613", "-123.9822356", "Cannon Beach", "Oregon", "Northern"),
                    new Beach ("Beach", "46.3596447", "-124.0816486", "Long Beach", "Washington", "Central"),
                    new Beach ("Beach", "46.8802148", "-124.2989708", "Ocean Shores", "Washington", "Central"),
                    new Beach ("Beach", "46.8526075", "-124.1303036", "Westport", "Washington", "Central"),
                    // new Beach ("Rivermouth", "47.9075214", "-124.645793", "La Push", "Washington", "Central"),
                };
                foreach(Beach b in Beaches)
                {
                    context.Beach.Add(b);
                }
                context.SaveChanges();
            }
        }
    }
}