using System.Collections.Generic;
using waveRiderTester.Models;

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