using System.Collections.Generic;
using waveRiderTester.Models;

// this class is designed to hold beach data and 45 day report data corresponding to that beach
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