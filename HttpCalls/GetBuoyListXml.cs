using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;

namespace waveRiderTester.HttpCalls
{
    public class GetBuoyListXml
    {
        public static async Task<XmlDocument> FetchAsync()
        {
            XmlDocument xDoc = new XmlDocument();  
            // create a new client
            using (HttpClient client = new HttpClient())
            // get response from client with desire path         
            using (HttpResponseMessage res = await client.GetAsync($"http://www.ndbc.noaa.gov/activestations.xml"))
            // store content into new httpContent class
            using (HttpContent content = res.Content)
            {
                string data = await content.ReadAsStringAsync();
                xDoc.LoadXml(data);
            }
            return xDoc;
        }
    }
}