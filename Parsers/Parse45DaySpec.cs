using System.Collections.Generic;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using waveRiderTester.CustomTypes;

// This class can be used to parse 45 day data from a spectral data text file

namespace waveRiderTester.Parsers
{
    public class Parse45DaySpec
    {
        // method required text data in string format as well as the buoy id
      public static List<SpecData> Get(string waveReportText, string buoyId)
        {
            // create an empty list to hold the hourly info report objects parsed from the text 
            List<SpecData> reports = new List<SpecData>();
            // a counter to keep track of which line in the text file we are at
            int count = 0;
            // string pattern to match each line from the text data. Each line contains the data 
            // for one hourly report 
            string pattern = @"\n\S+\s+\S+\s+\S+\s+\S+\s+\S+\s+\S+\s+\S+\s+\S+\s+\S+\s+\S+\s+\S+\s+\S+\s+\S+\s+\S+\s+\S+";   
            // itterate through the string pattern matches  
            foreach(Match m in Regex.Matches(waveReportText, pattern))
            {
                // increase count. The first two lines matched are just headings
                // and do not contain data
                count ++;
                if (count > 1)
                {
                    // create a list to hold all the individual data
                    // parsed from each line
                    List<string> properties = new List<string>();
                    // create another string pattern to match each reading
                    // from the report
                    string secondPattern = @"\S+";
                    // itterate through the pattern matches
                    foreach(Match mm in Regex.Matches(m.Value, secondPattern))
                    {
                        // add each peice of data to the list
                        properties.Add(mm.Value);
                    }
                    // use a constructor function to create a new instance of a 
                    // report object containing all the data from the current hourly report
                    SpecData report = new SpecData(properties[0], properties[1], properties[2],
                    properties[3], properties[4], properties[5], properties[6], properties[7],
                    properties[8], properties[9], properties[10], properties[11], properties[12],
                    properties[13], properties[14], buoyId);

                    // add the report to the list of reports
                    reports.Add(report);
                }
            }
            
            // return the reports
            return reports;
        }  
    }
}