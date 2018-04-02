// this class is designed to hold all of the standard report data for a single buoy

namespace waveRiderTester.CustomTypes
{
    public class StandardData
    {
        public string Year {get; set;}

        public string Month {get; set;}

        public string Day {get; set;}

        public string Hour {get; set;}
        
        public string Minute {get; set;}

        public string WindDirection {get; set;}

        public string WindSpeed {get; set;}

        public string GustSpeed {get; set;}

        public string WaveHeight {get; set;}

        public string DominantWavePeriod {get; set;}

        public string AverageWavePeriod {get; set;}

        public string WaveDirection {get; set;}

        public string SeaLevelPressure {get; set;}

        public string AirTemperature {get; set;}

        public string SeaSurfaceTemperature {get; set;}

        public string DewPointTemperature {get; set;}

        public string Visibility {get; set;}

        public string PressureTendancy {get; set;}

        public string Tide {get; set;}
        public string NbdcId {get; set;}

        public StandardData()
        {

        }
        public StandardData(string yr, string mnth, string dy, string hr,
        string mn, string wd, string ws, string gs, string wh, string dwp, 
        string avgwp, string wvd, string slp, string airtmp, string watertemp,
        string dewtemp, string vis, string pt, string tide, string bId)
        {
           Year = yr;
           Month = mnth;
           Day = dy;
           Hour = hr;
           Minute = mn;
           WindDirection = wd;
           WindSpeed = ws;
           GustSpeed = gs;
           WaveHeight = wh;
           DominantWavePeriod = dwp;
           AverageWavePeriod = avgwp;
           WaveDirection = wvd;
           SeaLevelPressure = slp;
           AirTemperature = airtmp;
           SeaSurfaceTemperature = watertemp;
           DewPointTemperature = dewtemp;
           Visibility = vis;
           PressureTendancy = pt;
           Tide = tide;
           NbdcId = bId;
        }
    }
}