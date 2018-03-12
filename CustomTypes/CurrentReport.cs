namespace waveRiderTester.CustomTypes
{
    public class CurrentReport
    {
        public StandardData CurrentStandardReport { get; set; }

        public SpecData CurrentSpecReport { get; set; }

        public string ContainsSpec { get; set; }

        public CurrentReport (StandardData std, SpecData spc)
        {
            CurrentStandardReport = std;
            CurrentSpecReport = spc;
            ContainsSpec = "true";
        }

        public CurrentReport (StandardData std)
        {
            CurrentStandardReport = std;
            ContainsSpec = "false";
        }

        public CurrentReport ()
        {
            
        }
    }
}