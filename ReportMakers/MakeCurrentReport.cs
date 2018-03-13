using System.Threading.Tasks;
using waveRiderTester.HttpCalls;
using waveRiderTester.CustomTypes;
using waveRiderTester.Parsers;
using waveRiderTester.Models;

namespace waveRiderTester.ReportMakers
{
    public class MakeCurrentReport
    {
        public static async Task<CurrentReport> GetAsync(Buoy buoy)
        {
            CurrentReport currentReport = new CurrentReport(buoy.Name, buoy.NbdcId);
            SpecData specData = new SpecData();

            string buoyStandardId = (buoy.NbdcId).ToUpper() + ".txt";
            string buoySpecId = (buoy.NbdcId).ToUpper() + ".spec";

            string standardReportText = await GetBuoyData.FetchAsync(buoyStandardId);
            string spectralReportText = await GetBuoyData.FetchAsync(buoySpecId);

            string firstCharSpec = (spectralReportText[0]).ToString();
            string firstCharStandard = (standardReportText[0].ToString());

            StandardData standardReport = ParseCurrentStandard.Get(standardReportText, buoy.NbdcId);

            if (firstCharSpec != "<" && firstCharStandard != "<")
            {
                specData = ParseCurrentSpec.Get(spectralReportText, buoy.NbdcId);
                currentReport = new CurrentReport(buoy.Name, buoy.NbdcId, standardReport, specData);
            }
            else if (firstCharStandard != "<")
            {
                currentReport = new CurrentReport(buoy.Name, buoy.NbdcId, standardReport);
            }

            return currentReport;
        }
    }
}