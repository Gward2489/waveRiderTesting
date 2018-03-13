using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Formatters.Json;
using waveRiderTester.HttpCalls;
using waveRiderTester.Parsers;
using waveRiderTester.CustomTypes;
using System.Collections.Generic;

namespace waveRiderTester.Controllers
{
     [Route("api/[controller]")]

    public class WaveController : Controller
    {

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            string results = await GetBuoyData.FetchAsync("");
            // SpecData report = ParseCurrentSpec.Get(results);
            List<StandardData> reports = Parse45DayStandard.Get(results, "buybuy");
            return Ok (reports);
        }

    }
}