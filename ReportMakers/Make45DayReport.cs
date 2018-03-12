using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using waveRiderTester.HttpCalls;
using waveRiderTester.CustomTypes;
using waveRiderTester.Parsers;

namespace waveRiderTester.ReportMakers
{
    public class Make45DayReport
    {
        public static async Task<FullReport> GetAsync(string buoyId)
        {
            FullReport fullReport = new FullReport();
            List<SpecData> spectralReports = new List<SpecData>(); 

            string buoyStandardId = buoyId + ".txt";
            string buoySpecId = buoyId + ".spec";

            string standardReportText = await GetBuoyData.FetchAsync(buoyStandardId);
            string spectralReportText = await GetBuoyData.FetchAsync(buoySpecId);

            string firstChar = (spectralReportText[0]).ToString();

            List<StandardData> standardReports = Parse45DayStandard.Get(standardReportText, buoyId);

            if (firstChar != "<")
            {
                spectralReports = Parse45DaySpec.Get(spectralReportText, buoyId);
                fullReport = new FullReport(standardReports, spectralReports);
            } 
            else 
            {
                fullReport = new FullReport(standardReports);
            }

            return fullReport;
        }
    }
}