using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using waveRiderTester.HttpCalls;
using waveRiderTester.CustomTypes;
using waveRiderTester.Parsers;
using waveRiderTester.Models;

namespace waveRiderTester.ReportMakers
{
    public class Make45DayReport
    {
        public static async Task<FullReport> GetAsync(Buoy buoy)
        {
            FullReport fullReport = new FullReport(buoy.Name, buoy.NbdcId);
            List<SpecData> spectralReports = new List<SpecData>(); 

            string buoyStandardId = (buoy.NbdcId).ToUpper() + ".txt";
            string buoySpecId = (buoy.NbdcId).ToUpper() + ".spec";

            string standardReportText = await GetBuoyData.FetchAsync(buoyStandardId);
            string spectralReportText = await GetBuoyData.FetchAsync(buoySpecId);

            string firstCharSpec = (spectralReportText[0]).ToString();
            string firstCharStandard = (standardReportText[0].ToString());

            List<StandardData> standardReports = Parse45DayStandard.Get(standardReportText, buoy.NbdcId);

            if (firstCharSpec != "<" && firstCharStandard !="<")
            {
                spectralReports = Parse45DaySpec.Get(spectralReportText, buoy.NbdcId);
                fullReport = new FullReport(buoy.Name, buoy.NbdcId, standardReports, spectralReports);
            } 
            else if (firstCharStandard != "<")
            {
                fullReport = new FullReport(buoy.Name, buoy.NbdcId, standardReports);
            }

            return fullReport;
        }
    }
}