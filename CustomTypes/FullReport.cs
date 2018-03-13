using System.Collections.Generic;

namespace waveRiderTester.CustomTypes
{
    public class FullReport
    {
        public string BuoyName { get; set; }
        public string NbdcId { get; set; }
        public List<StandardData> StandardReports { get; set;}
        public List<SpecData> SpectralReports { get; set; }
        public string ContainsSpec { get; set; }
        public string ContainsStandard { get; set; }

        public FullReport (string buoyName, string nbcdId, List<StandardData> std, 
        List<SpecData> spc)
        {
            BuoyName = buoyName;
            NbdcId = nbcdId;
            StandardReports = std;
            SpectralReports = spc;
            ContainsSpec = "true";
            ContainsStandard = "true";
        }

        public FullReport (string buoyName, string nbcdId, List<StandardData> std)
        {
            BuoyName = buoyName;
            NbdcId = nbcdId;
            StandardReports = std;
            ContainsSpec = "false";
            ContainsStandard = "true";
        }

        public FullReport()
        {
            ContainsSpec = "false";
            ContainsStandard = "false";
        }

        public FullReport(string buoyName, string nbcdId)
        {
            BuoyName = buoyName;
            NbdcId = nbcdId;
            ContainsSpec = "false";
            ContainsStandard = "false";
        }
    }
}