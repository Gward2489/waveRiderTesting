using System.Net.Http;
using System.Threading.Tasks;

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
                // Console.WriteLine(res);
                // Console.WriteLine(content);
                string data = await content.ReadAsStringAsync();
                // string line = data.Substring(140, 71);
                // Console.WriteLine(line);                                                
                buoyData = data;
            }
            return buoyData;
        }
    }
}