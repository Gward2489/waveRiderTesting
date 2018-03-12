using System.Threading.Tasks;
using waveRiderTester.HttpCalls;
using waveRiderTester.CustomTypes;
using waveRiderTester.Parsers;

namespace waveRiderTester.ReportMakers
{
    public class MakeCurrentReport
    {
        public static async Task<CurrentReport> GetAsync(string buoyId)
        {
            CurrentReport currentReport = new CurrentReport();
            SpecData specData = new SpecData();

            string buoyStandardId = buoyId + ".txt";
            string buoySpecId = buoyId + ".spec";

            string standardReportText = await GetBuoyData.FetchAsync(buoyStandardId);
            string spectralReportText = await GetBuoyData.FetchAsync(buoySpecId);

            string firstChar = (spectralReportText[0]).ToString();
            StandardData standardReport = ParseCurrentStandard.Get(standardReportText, buoyId);

            if (firstChar != "<")
            {
                SpecData spectralReport = ParseCurrentSpec.Get(spectralReportText, buoyId);
                currentReport = new CurrentReport(standardReport, spectralReport);
            }
            else
            {
                currentReport = new CurrentReport(standardReport);
            }

            return currentReport;
        }
    }
}