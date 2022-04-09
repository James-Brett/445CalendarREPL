using _445CalendarREPL;
using _445CalendarREPL.Models;
using System.Linq;
using Xunit;

namespace Tests
{
    public class CalendarServiceTests
    {
        [Fact]
        public void ReturnsFiscalYear()
        {
            var result = CalendarService.FourFourFiveCalendarForYear(1);
            Assert.NotNull(result);
        }

        [Fact]
        public void ReturnsFiscaleYearWith364Days()
        {
            var result = CalendarService.FourFourFiveCalendarForYear(1);
            var days = result.Months
                .SelectMany(m => m.Weeks)
                .SelectMany(w => w.Days);
            Assert.Equal(364, days.Count());
        }
    }
}