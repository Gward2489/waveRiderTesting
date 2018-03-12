using System.Collections.Generic;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using waveRiderTester.CustomTypes;

namespace waveRiderTester.Parsers
{
    public class Parse45DaySpec
    {
      public static List<SpecData> Get(string waveReportText, string buoyId)
        {
            List<SpecData> reports = new List<SpecData>();
            int count = 0;
            string pattern = @"\n\S+\s+\S+\s+\S+\s+\S+\s+\S+\s+\S+\s+\S+\s+\S+\s+\S+\s+\S+\s+\S+\s+\S+\s+\S+\s+\S+\s+\S+";            
            foreach(Match m in Regex.Matches(waveReportText, pattern))
            {
                count ++;
                if (count > 1)
                {
                    List<string> properties = new List<string>();
                    string secondPattern = @"\S+";
                    foreach(Match mm in Regex.Matches(m.Value, secondPattern))
                    {
                        properties.Add(mm.Value);
                    }
                    SpecData report = new SpecData(properties[0], properties[1], properties[2],
                    properties[3], properties[4], properties[5], properties[6], properties[7],
                    properties[8], properties[9], properties[10], properties[11], properties[12],
                    properties[13], properties[14], buoyId);
                    reports.Add(report);
                }
            }
            
            return reports;
        }  
    }
}