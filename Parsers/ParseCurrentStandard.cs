using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using waveRiderTester.CustomTypes;

namespace waveRiderTester.Parsers
{
    public class ParseCurrentStandard
    {
        public static StandardData Get(string waveReportText, string buoyId)
        {
            // Console.WriteLine(waveReportText);
            StandardData report = new StandardData();
            int count = 0;
            Console.WriteLine(waveReportText);
            string pattern = @"\n\S+\s+\S+\s+\S+\s+\S+\s+\S+\s+\S+\s+\S+\s+\S+\s+\S+\s+\S+\s+\S+\s+\S+\s+\S+\s+\S+\s+\S+\s+\S+\s+\S+\s+\S+\s+\S+";

            foreach(Match m in Regex.Matches(waveReportText, pattern))
            {
                Console.WriteLine(m.Value);
                count ++;
                List<string> properties = new List<string>();

                if (count == 2)
                {
                    string secondPattern = @"\S+";
                    foreach (Match mm in Regex.Matches(m.Value, secondPattern))
                    {
                            properties.Add(mm.Value);
                    }

                    report = new StandardData(properties[0], properties[1], properties[2],
                    properties[3], properties[4], properties[5], properties[6], properties[7],
                    properties[8], properties[9], properties[10], properties[11], properties[12],
                    properties[13], properties[14], properties[15], properties[16], properties[17],
                    properties[18], buoyId);
                }

                if (count == 2 )
                {
                    break;
                }

            }
            return report;
        }
    }
}