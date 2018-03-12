namespace waveRiderTester.CustomTypes
{
    public class SpecData
    {
        public string Year {get; set;}

        public string Month {get; set;}

        public string Day {get; set;}

        public string Hour {get; set;}

        public string Minute {get; set;}

        public string SignificantWaveHeight {get; set;}

        public string SwellHeight {get; set;}

        public string SwellPeriod {get; set;}

        public string WindWaveHeight {get; set;}

        public string WindWavePeriod {get; set;}

        public string SwellDirection {get; set;}

        public string WindWaveDirection {get; set;}

        public string Steepness {get; set;}

        public string AverageWavePeriod {get; set;}

        public string WaveDirection {get; set;}
        public string NbdcId {get; set;}

        public SpecData(string yr, string mth, string dy, 
        string hr, string min, string wh, string sh, string sp, 
        string wwh, string wwp, string sd, string wwd, string steep, string awp, string wd, string bId)
        {
            Year = yr;
            Month = mth;
            Day = dy;
            Hour = hr;
            Minute = min;
            SignificantWaveHeight = wh;
            SwellHeight = sh;
            SwellPeriod = sp;
            WindWaveHeight = wwh;
            WindWavePeriod = wwp;
            SwellDirection = sd;
            WindWaveDirection = wwd;
            Steepness = steep;
            AverageWavePeriod = awp;
            WaveDirection = wd;
            NbdcId = bId;
        }

        public SpecData()
        {

        }
    
    }


}