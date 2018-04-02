using System.Collections.Generic;
using waveRiderTester.Models;

// this class is designed to hold data for a beach and its current surf report data
 
namespace waveRiderTester.CustomTypes
{
    public class CurrentBeachReport
    {
        public Beach Beach { get; set; }

        public CurrentReport Report { get; set; }

        public CurrentBeachReport()
        {

        }
        
        public CurrentBeachReport(Beach beach, CurrentReport report)
        {
            Beach = beach;
            Report = report;
        }
    }
}