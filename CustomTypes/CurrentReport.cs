namespace waveRiderTester.CustomTypes
{
    public class CurrentReport
    {
        public string BuoyName { get; set; }
        public string NbdcId { get; set; }
        public StandardData CurrentStandardReport { get; set; }

        public SpecData CurrentSpecReport { get; set; }

        public string ContainsSpec { get; set; }

        public string ContainsStandard { get; set; }

        public CurrentReport (string buoyName, string nbcdId, StandardData std, SpecData spc)
        {
            BuoyName = buoyName;
            NbdcId = nbcdId;
            CurrentStandardReport = std;
            CurrentSpecReport = spc;
            ContainsSpec = "true";
            ContainsStandard = "true";
        }

        public CurrentReport (string buoyName, string nbcdId, StandardData std)
        {
            BuoyName = buoyName;
            NbdcId = nbcdId;
            CurrentStandardReport = std;
            ContainsSpec = "false";
            ContainsStandard = "true";
        }

        public CurrentReport ()
        {
            ContainsSpec = "false";
            ContainsStandard = "false";
        }

        public CurrentReport(string buoyName, string nbcdId)
        {
            BuoyName = buoyName;
            NbdcId = nbcdId;
            ContainsSpec = "false";
            ContainsStandard = "false";
        }
    }
}