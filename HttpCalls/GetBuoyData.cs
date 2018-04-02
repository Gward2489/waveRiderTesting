using System.Net.Http;
using System.Threading.Tasks;

// returns plain text file converted to string containing 45 days 
// worth of ocean data for given buoy

namespace waveRiderTester.HttpCalls
{
    public class GetBuoyData
    {
        public static async Task<string> FetchAsync(string buoyId)
        {
            string buoyData = "";
            // create a new client
            using (HttpClient client = new HttpClient())
            // get response from client with desire path         
            using (HttpResponseMessage res = await client.GetAsync($"http://www.ndbc.noaa.gov/data/realtime2/{buoyId}"))
            // store content into new httpContent class
            using (HttpContent content = res.Content)
            {
                string data = await content.ReadAsStringAsync();
                buoyData = data;
            }
            return buoyData;
        }
    }
}