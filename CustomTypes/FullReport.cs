using System.Collections.Generic;

namespace waveRiderTester.CustomTypes
{
    public class FullReport
    {
        public List<StandardData> StandardReports { get; set;}
        public List<SpecData> SpectralReports { get; set; }
        public string ContainsSpec { get; set; }

        public FullReport (List<StandardData> std, 
        List<SpecData> spc)
        {
            StandardReports = std;
            SpectralReports = spc;
            ContainsSpec = "true";
        }

        public FullReport (List<StandardData> std)
        {
            StandardReports = std;
            ContainsSpec = "false";
        }

        public FullReport()
        {
            
        }
    }
}