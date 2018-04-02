using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using waveRiderTester.HttpCalls;
using waveRiderTester.CustomTypes;
using waveRiderTester.Parsers;
using waveRiderTester.Models;

// this class is used to to create a 45 day report object and populate it with data

namespace waveRiderTester.ReportMakers
{
    public class Make45DayReport
    {
        public static async Task<FullReport> GetAsync(Buoy buoy)
        {
            // create instance of report object, adding buoy name and id
            FullReport fullReport = new FullReport(buoy.Name, buoy.NbdcId);
            // create a list to hold the spectral report data
            List<SpecData> spectralReports = new List<SpecData>(); 
            // create strings that will be used to complete get request urls.
            string buoyStandardId = (buoy.NbdcId).ToUpper() + ".txt";
            string buoySpecId = (buoy.NbdcId).ToUpper() + ".spec";
            // make async calls to retreive buoy data in string format
            string standardReportText = await GetBuoyData.FetchAsync(buoyStandardId);
            string spectralReportText = await GetBuoyData.FetchAsync(buoySpecId);
            // get first character from report strings to use as check for succesful request
            string firstCharSpec = (spectralReportText[0]).ToString();
            string firstCharStandard = (standardReportText[0].ToString());

            // parse standard reports and store them in list
            List<StandardData> standardReports = Parse45DayStandard.Get(standardReportText, buoy.NbdcId);

            // if the first character of the string is an '<' that means the http 
            // response resulted in an xml response stating no data found for url given
            
            // if neither request had xml...
            if (firstCharSpec != "<" && firstCharStandard !="<")
            {
                // parse spectral data make new object with entire report
                spectralReports = Parse45DaySpec.Get(spectralReportText, buoy.NbdcId);
                fullReport = new FullReport(buoy.Name, buoy.NbdcId, standardReports, spectralReports);
            } 
            // else, if the standard report contained no xml
            else if (firstCharStandard != "<")
            {
                // store parsed standard data in object with buoy name and id
                fullReport = new FullReport(buoy.Name, buoy.NbdcId, standardReports);
            }

            // return 45 day report
            return fullReport;
        }
    }
}