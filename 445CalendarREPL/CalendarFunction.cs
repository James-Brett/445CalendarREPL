using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace _445CalendarREPL
{
    public static class CalendarFunction
    {
        [FunctionName("CalendarFunction")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var yearString = req.Query["year"];
            if (!int.TryParse(yearString, out var year) || year < 0)
            {
                return new BadRequestObjectResult($"{yearString} could not be parsed into a valid calendar year");
            }

            var calendar = CalendarService.FourFourFiveCalendarForYear(year);

            return new OkObjectResult(calendar);
        }
    }
}
