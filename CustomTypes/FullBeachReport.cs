using System.Collections.Generic;
using waveRiderTester.Models;

namespace waveRiderTester.CustomTypes
{
    public class FullBeachReport
    {
        public Beach Beach { get; set; }

        public List<FullReport> Reports { get; set; }

        public FullBeachReport()
        {

        }

        public FullBeachReport(Beach beach, List<FullReport> reports)
        {
            Beach = beach;
            Reports = reports;
        }
    }
}