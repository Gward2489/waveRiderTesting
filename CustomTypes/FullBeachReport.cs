using System.Collections.Generic;
using waveRiderTester.Models;

namespace waveRiderTester.CustomTypes
{
    public class FullBeachReport
    {
        public Beach Beach { get; set; }

        public FullReport Report { get; set; }

        public FullBeachReport()
        {

        }

        public FullBeachReport(Beach beach, FullReport report)
        {
            Beach = beach;
            Report = report;
        }
    }
}