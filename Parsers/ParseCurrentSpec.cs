using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using waveRiderTester.CustomTypes;

// This method is used to parse only the most current spectral report data from a buoy

namespace waveRiderTester.Parsers
{
    public class ParseCurrentSpec
    {
        public static SpecData Get(string waveReportText, string buoyId)
        {
            Console.WriteLine(waveReportText);
            int count = 0;
            string pattern = @"\n\S+\s+\S+\s+\S+\s+\S+\s+\S+\s+\S+\s+\S+\s+\S+\s+\S+\s+\S+\s+\S+\s+\S+\s+\S+\s+\S+\s+\S+";
            SpecData report = new SpecData();
            foreach (Match m in Regex.Matches(waveReportText, pattern))
            {
                count ++;
                List<string> properties = new List<string>();
                
                // the third line is the first line of data, so check the counter
                // for a value of 2
                if (count == 2)
                {
                    string secondPattern = @"\S+";
                    foreach (Match mm in Regex.Matches(m.Value, secondPattern))
                    {
                        string prop = mm.Value;
                        properties.Add(prop);
                    }   

                    report = new SpecData(properties[0], properties[1],  
                    properties[2], properties[3], properties[4], 
                    properties[5], properties[6], properties[7], 
                    properties[8], properties[9], properties[10],
                    properties[11], properties[12], properties[13], 
                    properties[14], buoyId);
                } 
                
                // break the loop after it reads the third line
                if (count == 2 )
                {
                    break;
                }
            }
            return report;
        }
    }
}