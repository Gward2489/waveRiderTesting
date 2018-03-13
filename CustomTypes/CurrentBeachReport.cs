using System.Collections.Generic;
using waveRiderTester.Models;

namespace waveRiderTester.CustomTypes
{
    public class CurrentBeachReport
    {
        public Beach Beach { get; set; }

        public List<CurrentReport> Reports { get; set; }

        public CurrentBeachReport()
        {

        }
        
        public CurrentBeachReport(Beach beach, List<CurrentReport> reports)
        {
            Beach = beach;
            Reports = reports;
        }
    }
}