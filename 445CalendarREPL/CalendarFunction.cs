using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;

namespace _445CalendarREPL
{
    public static class CalendarFunction
    {
        [FunctionName("FourFourFiveCalendar")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req)
        {
            var yearString = req.Query["year"];
            if (!int.TryParse(yearString, out var year) || year < 0)
            {
                return new BadRequestObjectResult($"'{yearString}' could not be parsed into a valid calendar year");
            }

            var calendar = CalendarService.FourFourFiveCalendarForYear(year);

            return new OkObjectResult(calendar);
        }
    }
}
